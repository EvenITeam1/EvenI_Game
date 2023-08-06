using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManage : MonoBehaviour
{
    [SerializeField] List<GameObject> PopUpCanvasList;
    [SerializeField] List<GameObject> ButtonList;
    [SerializeField] GameObject WarningCanvas;
    [SerializeField] GameObject SuccessCanvas;

    public static int selectedDogIndex;
    public static int selectedSection;
    public void SetOnOff()
    {
        for (int i = 0; i < PopUpCanvasList.Count; i++)
        {
            if (PopUpCanvasList[i].activeSelf)
            {
                PopUpCanvasList[i].SetActive(false);
                return;
            }
        }
    }

    public void ButtonOff()
    {
        for (int i = 0; i < ButtonList.Count; i++)
            ButtonList[i].SetActive(false);
    }

    public void ChangeSection(int index)
    {
        selectedSection = index;
    }

    public void ChangeDogIndex(int index)
    {
        selectedDogIndex = index;
    }

    public void BuyWithCoin()
    {
        switch(selectedSection)
        {
            case 0:
                DogBuyWithCoin();
                break;

            case 1:
                RevivalBuyWithCoin();
                break;

           case 2:
                SatietyBuyWithCoin();
                break;
        }
    }

    void DogBuyWithCoin()
    {
        int collectedCoin = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin;

        if (collectedCoin < 55000)
        {
            WarningCanvas.SetActive(true);
        }

        else
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 55000;
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CharacterUnlockData[selectedDogIndex] = true;
            SuccessCanvas.SetActive(true);
        }
    }

    void SatietyBuyWithCoin()
    {
        int collectedCoin = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin;

        if (collectedCoin < 2500)
        {
            WarningCanvas.SetActive(true);
        }

        else
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 2500;
            //실제 내용
            SuccessCanvas.SetActive(true);
        }
    }

    void RevivalBuyWithCoin()
    {
        int collectedCoin = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin;

        if (collectedCoin < 5000)
        {
            WarningCanvas.SetActive(true);
        }

        else
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 5000;
            //실제 내용
            SuccessCanvas.SetActive(true);
        }
    }
}
