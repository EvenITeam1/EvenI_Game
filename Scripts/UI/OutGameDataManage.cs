using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OutGameDataManage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmount;
    [SerializeField] TextMeshProUGUI expAmount;
    [SerializeField] List<CharacterUI> characterUIList;//use only in MyDogScene
    [SerializeField] List<Button> stageEnterButtonList; //use only in StageSelectScene

    private static bool isFirstAccess = true;

    private void Awake()
    {
        if (isFirstAccess)
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.LoadSatietyDataFromJson();
            isFirstAccess = false;
        }        
    }

    private void FixedUpdate()
    {
        if (goldAmount != null) { goldAmount.text = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin.ToString(); }
        if (expAmount != null) { expAmount.text = "EXP " + GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp.ToString() + "/1000"; }

        if(characterUIList[0] != null)
        {
            characterUIList[0].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isShibaUnlocked;
            characterUIList[1].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenRetrieverUnlocked;
            characterUIList[2].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isLabradorRetrieverUnlocked;
            characterUIList[3].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGreyHoundUnlocked;
            characterUIList[4].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGermanShepherdUnlocked;
            characterUIList[5].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isHuskyUnlocked;
            characterUIList[6].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWolfUnlocked;
            characterUIList[7].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenPomeranianUnlocked;
            characterUIList[8].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWhitePomeranianUnlocked;
            characterUIList[9].isUnlocked = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isPugUnlocked;
        }
       
        for (int i = 0; i < stageEnterButtonList.Count; i++)
        {
            if (stageEnterButtonList[i] == null)
                break;

            else if (stageEnterButtonList[i] != null)
            {
                if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked < i)
                {
                    stageEnterButtonList[i].interactable = false;
                    stageEnterButtonList[i].gameObject.GetComponent<Image>().color = Color.black;
                }

                else
                {
                    stageEnterButtonList[i].interactable = true;
                    stageEnterButtonList[i].gameObject.GetComponent<Image>().color = Color.white;
                }
            }              
        }
    }
}
