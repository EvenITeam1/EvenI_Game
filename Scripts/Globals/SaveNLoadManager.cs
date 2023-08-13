using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using System.IO;


/*********************************************************************************/

#region Ingame 
[System.Serializable]
public class IngameSaveData
{
    public bool IsIngameStart;
    public int CurrentStageNumber;
    public int CurrentPhaseNumber;
    /// <summary>
    /// 보스 클리어와 동일하다 생각됨..
    /// </summary>
    public bool IsStageClear;
    public bool IsRevivalChecked;

    public float PrevHP;
    public int CollectedScore;
    public bool isCoinBoostOn;
    public int RevivalCount; 
    public IngameSaveData()
    {
        PrevHP = -1;
        IsStageClear = false;
        isCoinBoostOn = false;
        CollectedScore = -1;
        RevivalCount = -1;       
    }

    //입장 버튼
    public void ClearIngameData()
    {
        IsIngameStart = false;
        CurrentStageNumber = -1;
        CurrentPhaseNumber = -1;
        IsStageClear = false;
        PrevHP = -1;
        CollectedScore = 0;
        RevivalCount = 0;
    }
}
#endregion


/*********************************************************************************/

#region Outgame
[System.Serializable]
public class OutgameSaveData
{
    public int AdditionalRevivalCount;
    public int Coin15Count;
    public bool IsNoAdActivated;

    #region AccountData

    public int AccountLevel;
    public int CollectedExp;
    public DOG_INDEX SelectedPlayerINDEX;
    public int SelectedIconINDEX;

    #endregion

    #region ShopData
    public int CollectedCoin;   // 로비에서 코인이 될듯.
    public int CollectedBone;

    #endregion

    #region  AchievementData

    public int HightestStageUnlocked;

    #endregion


    #region CharacterUnlockData

    public bool isShibaUnlocked;
    public bool isGoldenRetrieverUnlocked;
    public bool isLabradorRetrieverUnlocked;
    public bool isGreyHoundUnlocked;
    public bool isGermanShepherdUnlocked;
    public bool isHuskyUnlocked;
    public bool isWolfUnlocked;
    public bool isGoldenPomeranianUnlocked;
    public bool isWhitePomeranianUnlocked;
    public bool isPugUnlocked;
    #endregion

    #region SoundData
    public float BGMAmount;
    public bool IsBGMMuted = false;
    public float SFXAmount;
    public bool IsSFXMuted = false;
    #endregion

    [SerializeField]
    public OutgameSaveData(
        int reviveCount, int coin15Count, bool isNoAd, int level, int CollectedExp, DOG_INDEX selectedPlayerIndex, int selectedIconIndex,
        int coin, int bone,
        int hightestUnlockedStage,
        bool one, bool two, bool three, bool four, bool five, bool six, bool seven, bool eight, bool nine, bool ten,
        float BGM, float SFX, bool isBGMMuted, bool isSFXMuted
    )
    {
        this.AdditionalRevivalCount = reviveCount;
        this.Coin15Count = coin15Count; 
        this.IsNoAdActivated = isNoAd;
        this.AccountLevel = level;
        this.CollectedExp = CollectedExp;
        this.SelectedPlayerINDEX = selectedPlayerIndex;
        this.SelectedIconINDEX = selectedIconIndex;
        this.CollectedCoin = coin;
        this.CollectedBone = bone;
        this.HightestStageUnlocked = hightestUnlockedStage;
        this.isShibaUnlocked = one;
        this.isGoldenRetrieverUnlocked = two;
        this.isLabradorRetrieverUnlocked = three;
        this.isGreyHoundUnlocked = four;
        this.isGermanShepherdUnlocked = five;
        this.isHuskyUnlocked = six;
        this.isWolfUnlocked = seven;
        this.isGoldenPomeranianUnlocked = eight;
        this.isWhitePomeranianUnlocked = nine;
        this.isPugUnlocked = ten;
        this.BGMAmount = BGM;
        this.SFXAmount = SFX;
        this.IsBGMMuted = isBGMMuted;
        this.IsSFXMuted = isSFXMuted;
    }
    
    public void ClearJson() {
        if (File.Exists(string.Format("{0}/{1}.json", Application.persistentDataPath, "OutgameData")))
        {
            File.Delete(string.Format("{0}/{1}.json", Application.persistentDataPath, "OutgameData"));
        }
    }

    public void SaveOutgameDataToJson()
    {
        OutgameSaveData outgameSaveData = new OutgameSaveData(
            AdditionalRevivalCount, Coin15Count, IsNoAdActivated, AccountLevel, CollectedExp, SelectedPlayerINDEX, SelectedIconINDEX, 
            CollectedCoin, CollectedBone, 
            HightestStageUnlocked,
            isShibaUnlocked, isGoldenRetrieverUnlocked, isLabradorRetrieverUnlocked, isGreyHoundUnlocked, isGermanShepherdUnlocked, isHuskyUnlocked, isWolfUnlocked, isGoldenPomeranianUnlocked, isWhitePomeranianUnlocked, isPugUnlocked,
            BGMAmount, SFXAmount, IsBGMMuted, IsSFXMuted
        );
        var result = JsonConvert.SerializeObject(outgameSaveData);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.persistentDataPath, "OutgameData"), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(result);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
    public void LoadOutgameDataFromJson()
    {
        if (!File.Exists(string.Format("{0}/{1}.json", Application.persistentDataPath, "OutgameData")))
        {
            //Debug.Log("앱 최초실행 : 아웃게임 스크립트");
            AdditionalRevivalCount = 0;
            Coin15Count = 0;
            IsNoAdActivated = false;
            AccountLevel = 1;
            CollectedExp = 0;
            SelectedPlayerINDEX = DOG_INDEX.C_01;
            SelectedIconINDEX = 0;
            CollectedCoin = 0;
            CollectedBone = 0;
            HightestStageUnlocked = 0;
            isShibaUnlocked = true;
            isGoldenRetrieverUnlocked = false;
            isLabradorRetrieverUnlocked = false;
            isGreyHoundUnlocked = false;
            isGermanShepherdUnlocked = false;
            isHuskyUnlocked = false;
            isWolfUnlocked = false;
            isGoldenPomeranianUnlocked = false;
            isWhitePomeranianUnlocked = false;
            isPugUnlocked = false;
            BGMAmount = SFXAmount = 1f;
            IsBGMMuted = IsSFXMuted = false;
        }

        else
        { 
            string JsonFileText = File.ReadAllText(string.Format("{0}/{1}.json", Application.persistentDataPath, "OutgameData"));
            OutgameSaveData OutgameData = JsonConvert.DeserializeObject<OutgameSaveData>(JsonFileText);
            AdditionalRevivalCount = OutgameData.AdditionalRevivalCount;
            Coin15Count = OutgameData.Coin15Count;
            IsNoAdActivated = OutgameData.IsNoAdActivated;
            AccountLevel = OutgameData.AccountLevel;
            CollectedExp = OutgameData.CollectedExp;
            SelectedPlayerINDEX = OutgameData.SelectedPlayerINDEX;
            SelectedIconINDEX = OutgameData.SelectedIconINDEX;
            CollectedCoin = OutgameData.CollectedCoin;
            CollectedBone = OutgameData.CollectedBone;
            HightestStageUnlocked = OutgameData.HightestStageUnlocked;
            isShibaUnlocked = OutgameData.isShibaUnlocked;
            isGoldenRetrieverUnlocked = OutgameData.isGoldenRetrieverUnlocked;
            isLabradorRetrieverUnlocked = OutgameData.isLabradorRetrieverUnlocked;
            isGreyHoundUnlocked = OutgameData.isGreyHoundUnlocked;
            isGermanShepherdUnlocked = OutgameData.isGermanShepherdUnlocked;
            isHuskyUnlocked = OutgameData.isHuskyUnlocked;
            isWolfUnlocked = OutgameData.isWolfUnlocked;
            isGoldenPomeranianUnlocked = OutgameData.isGoldenPomeranianUnlocked;
            isWhitePomeranianUnlocked = OutgameData.isWhitePomeranianUnlocked;
            isPugUnlocked = OutgameData.isPugUnlocked;
            BGMAmount = OutgameData.BGMAmount;
            SFXAmount = OutgameData.SFXAmount;
            IsBGMMuted = OutgameData.IsBGMMuted;
            IsSFXMuted = OutgameData.IsSFXMuted;
        }
    }
}

#endregion


/*********************************************************************************/

[System.Serializable]
public class SaveData
{
    /*인게임 데이터가 아닌 아웃게임 데이터 위주로 기술할것. 다만 일단 합치고 생각한다.*/

    [SerializeField]
    public IngameSaveData ingameSaveData;

    [SerializeField]
    public OutgameSaveData outgameSaveData;

    public SaveData()
    {
    }
}


/*********************************************************************************/

public class SaveNLoadManager : MonoBehaviour
{
    [SerializeField]
    public SaveData saveData;

    public ref SaveData GetSaveDataByRef()
    {
        return ref this.saveData;
    }
    [ContextMenu("Clear Json")]
    public void ClearJson() {
        //Debug.Log(Application.persistentDataPath);
        if(File.Exists(string.Format("{0}/{1}.json", Application.persistentDataPath, "NickNameJsonData"))){
            File.Delete(string.Format("{0}/{1}.json", Application.persistentDataPath, "NickNameJsonData"));
            File.Delete(string.Format("{0}/{1}.json", Application.persistentDataPath, "OutgameData"));
            File.Delete(string.Format("{0}/{1}.json", Application.persistentDataPath, "QuitTimeData"));
        }
    }
}
