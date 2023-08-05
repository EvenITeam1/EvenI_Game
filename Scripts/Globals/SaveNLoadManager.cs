using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*********************************************************************************/

#region Ingame 
[System.Serializable]
public class IngameSaveData
{
    public float PrevHP;
    /// <summary>
    /// 보스 클리어와 동일하다 생각됨..
    /// </summary>
    public bool IsStageClear;
    public int CollectedScore;
    public int RevivalCount;
    public IngameSaveData()
    {
        PrevHP = -1;
        IsStageClear = false;
        CollectedScore = -1;
        RevivalCount = -1;
    }
}
#endregion


/*********************************************************************************/

#region Outgame
[System.Serializable]
public class OutgameSaveData
{
    // public int RevivalCount; ??

    #region AccountData
        
        public int AccountLevel;
        public int CollectedExp;
        public DOG_INDEX SelectedPlayerINDEX;
        public int SelectedIconINDEX;
    
    #endregion

    #region ShopData
        public int CollectedCoin;   // 로비에서 코인이 될듯.

    #endregion

    #region  AchievementData
    
        public List<bool> StageClearData;
        public List<bool> CharacterUnlockData;
    
    #endregion

    [SerializeField]
    public OutgameSaveData()
    {
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
}