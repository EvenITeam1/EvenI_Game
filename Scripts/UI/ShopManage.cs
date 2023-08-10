using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManage : MonoBehaviour
{
    [SerializeField] List<GameObject> PopUpCanvasList;
    [SerializeField] List<GameObject> ButtonList;
    [SerializeField] GameObject WarningCanvas;
    [SerializeField] GameObject AlreadyCanvas;
    [SerializeField] GameObject SuccessCanvas;
    [SerializeField] GameObject PurchaseFailedCanvas;

    public static int selectedDogIndex;
    public static int selectedSection;

    #region ButtonSet
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

    #endregion

    #region CoinBonePurchase
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

    public void BuyWithBone()
    {
        switch (selectedSection)
        {
            case 0:
                DogBuyWithBone();
                break;
            case 1:
                RevivalBuyWithBone();
                break;
            case 2:
                SatietyBuyWithBone();
                break;
            case 3:
                Coin15BuyWithBone();
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
            switch (selectedDogIndex)
            {
                case 1:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenRetrieverUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenRetrieverUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 55000;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 2:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isLabradorRetrieverUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isLabradorRetrieverUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 55000;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 3:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGreyHoundUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGreyHoundUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 55000;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 4:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGermanShepherdUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGermanShepherdUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 55000;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 7:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenPomeranianUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenPomeranianUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 55000;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 8:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWhitePomeranianUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWhitePomeranianUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 55000;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 9:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isPugUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isPugUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin -= 55000;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
            }
        }
    }

    void DogBuyWithBone()
    {
        int collectedBone = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone;

        if (collectedBone < 169)
        {
            WarningCanvas.SetActive(true);
        }

        else
        {
            switch (selectedDogIndex)
            {
                case 1:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenRetrieverUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenRetrieverUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 169;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }       
                    break;
                case 2:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isLabradorRetrieverUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isLabradorRetrieverUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 169;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 3:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGreyHoundUnlocked)
                        AlreadyCanvas.SetActive(true);            
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGreyHoundUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 169;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 4:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGermanShepherdUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGermanShepherdUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 169;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 7:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenPomeranianUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenPomeranianUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 169;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 8:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWhitePomeranianUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWhitePomeranianUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 169;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
                case 9:
                    if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isPugUnlocked)
                        AlreadyCanvas.SetActive(true);
                    else
                    {
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isPugUnlocked = true;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 169;
                        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
                        SuccessCanvas.SetActive(true);
                    }
                    break;
            }
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
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
            SatietyManage.GainChargeCount(1);
            SuccessCanvas.SetActive(true);
        }
    }
    void SatietyBuyWithBone()
    {
        int collectedBone = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone;

        if (collectedBone < 7)
        {
            WarningCanvas.SetActive(true);
        }

        else
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 7;
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
            SatietyManage.GainChargeCount(1);
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
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount++;
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
            SuccessCanvas.SetActive(true);
        }
    }
    void RevivalBuyWithBone()
    {
        int collectedBone = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone;

        if (collectedBone < 13)
        {
            WarningCanvas.SetActive(true);
        }

        else
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 13;
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount++;
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
            SuccessCanvas.SetActive(true);
        }
    }

    void Coin15BuyWithBone()
    {
        int collectedBone = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone;

        if (collectedBone < 23)
        {
            WarningCanvas.SetActive(true);
        }

        else
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone -= 23;
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.Coin15Count++;
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
            SuccessCanvas.SetActive(true);
        }
    }
    #endregion

    #region WonBuyFunctions
    public void GetPackage1()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin += 1000;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount++;
        SatietyManage.GainChargeCount(1);
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
        SuccessCanvas.SetActive(true);
    }

    public void GetPackage2()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin += 9000;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone += 30;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount += 3;
        SatietyManage.GainChargeCount(3);
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
        SuccessCanvas.SetActive(true);
    }
    public void GetPackage3()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin += 10000;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone += 70;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount += 5;
        SatietyManage.GainChargeCount(5);
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
        SuccessCanvas.SetActive(true);
    }

    public void GetNoAd()
    {
        //NoAd
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone += 50;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
        SuccessCanvas.SetActive(true);
    }
    public void HuskyCheck()
    {
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isHuskyUnlocked)
            AlreadyCanvas.SetActive(true);
    }

    public void GetHusky()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isHuskyUnlocked = true;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
        SuccessCanvas.SetActive(true);
    }

    public void WolfCheck()
    {
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWolfUnlocked)
            AlreadyCanvas.SetActive(true);
    }
    public void GetWolf()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWolfUnlocked = true;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
        SuccessCanvas.SetActive(true);
    }

    public void GetBone(int amount)
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedBone += amount;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
        SuccessCanvas.SetActive(true);
    }
    public void PurchaseFailed()
    {
        PurchaseFailedCanvas.SetActive(true);
    }

    #endregion
}
