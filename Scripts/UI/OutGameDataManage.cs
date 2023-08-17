using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OutGameDataManage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmount;
    [SerializeField] TextMeshProUGUI expAmount;
    [SerializeField] TextMeshProUGUI levelCount;
    [SerializeField] List<int> RequireExpAmounts;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI boneAmount;
    [SerializeField] TextMeshProUGUI additionalReviveAmount;
    [SerializeField] TextMeshProUGUI coin15Amount; 
    [SerializeField] List<CharacterUI> characterUIList;//use only in MyDogScene
    [SerializeField] List<Button> stageEnterButtonList; //use only in StageSelectScene
    public int currentLevel = 1;

    private static bool isFirstAccess = true;

    private void Awake()
    {
        if (isFirstAccess)
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.LoadOutgameDataFromJson();
            isFirstAccess = false;
        }
        currentLevel = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AccountLevel;
        SetExpRequiredAmount();
    }

    private void FixedUpdate()
    {
        if (goldAmount != null) { goldAmount.text = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin.ToString(); }
        if (boneAmount != null) { boneAmount.text = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone.ToString(); }
        if (expAmount != null) { expAmount.text = "EXP " + GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp.ToString() + "/" + RequireExpAmounts[currentLevel - 1].ToString(); }
        if(expBar != null) { expBar.value = (float)GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp / (float)RequireExpAmounts[currentLevel - 1]; }
        if(levelCount != null) { levelCount.text = "Level " + currentLevel; }
        if (additionalReviveAmount != null) { additionalReviveAmount.text = "+" + GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount.ToString(); }
        if (coin15Amount != null) { coin15Amount.text = "+" + GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.Coin15Count.ToString(); }

        if (characterUIList[0] != null)
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

        for (int i = 0; i < 2; i++)
        {
            if (stageEnterButtonList[i] == null)
                break;

            stageEnterButtonList[i].interactable = true;
            stageEnterButtonList[i].gameObject.GetComponent<Image>().color = Color.white;
        }

        for (int i = 2; i < stageEnterButtonList.Count; i++)
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

    public void SetExpRequiredAmount()
    {
        RequireExpAmounts[0] = 30;
        RequireExpAmounts[1] = 45;
        RequireExpAmounts[2] = 60;
        RequireExpAmounts[3] = 75;
        RequireExpAmounts[4] = 90;
        RequireExpAmounts[5] = 105;
        RequireExpAmounts[6] = 120;
        RequireExpAmounts[7] = 135;
        RequireExpAmounts[8] = 150;
        RequireExpAmounts[9] = 165;
        RequireExpAmounts[10] = 180;
        RequireExpAmounts[11] = 195;
        RequireExpAmounts[12] = 210;
        RequireExpAmounts[13] = 225;
        RequireExpAmounts[14] = 240;
        RequireExpAmounts[15] = 255;
        RequireExpAmounts[16] = 270;
        RequireExpAmounts[17] = 285;
        RequireExpAmounts[18] = 300;
        RequireExpAmounts[19] = 315;
    }
}
