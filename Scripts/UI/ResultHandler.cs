using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public enum RESULT_INDEX
{
    SCORE = 0, EXP, COIN
}

[DisallowMultipleComponent]
public class ResultHandler : MonoBehaviour
{
    public float WinExpRatio = 0.01f;
    public float WinCoinRatio = 1f;
    public float LoseExpRatio = 0.0025f;
    public float LoseCoinRatio = 0.05f;
    public float CoinBoostRatio = 1f;
    public static int EnteredStageIndex;
    [SerializeField] List<int> RequireExpAmounts;

    /*
    내부 데이터를 가져오고 외부 데이터로 내보낸다.
    그건 SaveNLoadManager을 통해서 가져오고 내보내면 될것 같다.

    마치 DataTrigger과 같지만, 데이터 트리거는 RunnerManager에 의존한다.
    따라서 이건 오직 SaveNLoadManager을 통해서 데이터 교환이 일어나겠다.
    */
    public List<TextRoulette> textRoulettes;

    private void Start()
    {
        GameManager.Instance.GlobalSoundManager.ClearAudioSetting();
        bool IsClearState = true;

        bool IsCoinBoostOn = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.isCoinBoostOn;
        if(IsCoinBoostOn) { GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.Coin15Count--; }

        if (SceneManager.GetActiveScene().name == "GameOverScene") { IsClearState = false; }
        
        CoinBoostRatio = IsCoinBoostOn ? 1.5f : 1f;

          float Score = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore;
        if((int)Score == -1) {Score = 0;} 
        textRoulettes[(int)RESULT_INDEX.SCORE].CountingNumber = (int)Score;

        textRoulettes[(int)RESULT_INDEX.EXP].CountingNumber
           = (int)((IsClearState) ? (WinExpRatio * Score) : (LoseExpRatio * Score));

        textRoulettes[(int)RESULT_INDEX.COIN].CountingNumber
            = (int)((IsClearState) ? (WinCoinRatio * Score * CoinBoostRatio) : (LoseCoinRatio * Score * CoinBoostRatio));

        int usedReviveCount = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount + 3 - GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.RevivalCount;

        if(usedReviveCount > 3)
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount -= usedReviveCount - 3;
        }

        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin += (int)((IsClearState) ? (WinCoinRatio * Score * CoinBoostRatio) : (LoseCoinRatio * Score * CoinBoostRatio));
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp += (int)((IsClearState) ? (WinExpRatio * Score) : (LoseExpRatio * Score));

        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked == EnteredStageIndex && IsClearState)
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked++;

        SetExpRequiredAmount();
        calculateLevel();

        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
    }

    public static void SendEnterStageData(int num)//used in StageSelectScene
    {
        EnteredStageIndex = num;
    }

    public void calculateLevel()
    {
        while(true)
        {
            int currentLevel = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AccountLevel;
            int currentExp = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp;
            int requireExpAmountToNextLevel = RequireExpAmounts[currentLevel - 1];

            if (currentLevel == 21)
                break;

            else if (currentExp > requireExpAmountToNextLevel)
            {
                GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AccountLevel++;
                GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp -= requireExpAmountToNextLevel;
            }

            else
            {
                break;
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
        RequireExpAmounts[20] = 315;
    }
}
