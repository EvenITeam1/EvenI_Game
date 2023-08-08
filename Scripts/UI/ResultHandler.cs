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
    public static int EnteredStageIndex;

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
        if (SceneManager.GetActiveScene().name == "GameOverScene") { IsClearState = false; }
        float Score = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore;
        if((int)Score == -1) {Score = 0;} 
        textRoulettes[(int)RESULT_INDEX.SCORE].CountingNumber = (int)Score;

        textRoulettes[(int)RESULT_INDEX.EXP].CountingNumber
           = (int)((IsClearState) ? (WinExpRatio * Score) : (LoseExpRatio * Score));

        textRoulettes[(int)RESULT_INDEX.COIN].CountingNumber
            = (int)((IsClearState) ? (WinCoinRatio * Score) : (LoseCoinRatio * Score));

        int usedReviveCount = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount + 3 - GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.RevivalCount;

        if(usedReviveCount > 3)
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.AdditionalRevivalCount -= usedReviveCount - 3;
        }

        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin += (int)((IsClearState) ? (WinCoinRatio * Score) : (LoseCoinRatio * Score));
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp += (int)((IsClearState) ? (WinExpRatio * Score) : (LoseExpRatio * Score));
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked == EnteredStageIndex && IsClearState)
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked++;

        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SaveOutgameDataToJson();
    }

    public static void SendEnterStageData(int num)//used in StageSelectScene
    {
        EnteredStageIndex = num;
    }

}
