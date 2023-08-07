using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconApply : MonoBehaviour
{
    [SerializeField] List<GameObject> IconList = new List<GameObject>(10);
    [SerializeField] List<Image> userIcons;
    private int index;

    private void Awake()
    {
        InitializeIcon();
        SetIconAccess();
    }
    private void Update()
    {
        SetOutline();
    }

    void SetOutline()
    {
        for (int i = 0; i < IconList.Count; i++)
            IconList[i].GetComponent<Outline>().enabled = false;

        IconList[index].GetComponent<Outline>().enabled = true;
    }

    void InitializeIcon()
    {
        index = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedIconINDEX;
        for (int i = 0; i < userIcons.Count; i++)
        {
            userIcons[i].sprite = IconList[index].GetComponent<Image>().sprite;
        }
    }

    void SetIconAccess()
    {
        IconList[0].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isShibaUnlocked;
        IconList[1].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenRetrieverUnlocked;
        IconList[2].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isLabradorRetrieverUnlocked;
        IconList[3].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGreyHoundUnlocked;
        IconList[4].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGermanShepherdUnlocked;
        IconList[5].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isHuskyUnlocked;
        IconList[6].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWolfUnlocked;
        IconList[7].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenPomeranianUnlocked;
        IconList[8].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWhitePomeranianUnlocked;
        IconList[9].GetComponent<Button>().interactable = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isPugUnlocked;
    }


    public void ApplyIcon(int n)
    {
        for (int i = 0; i < userIcons.Count; i++)
            userIcons[i].sprite = IconList[n].GetComponent<Image>().sprite;

        index = n;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedIconINDEX = n;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
    }
}
