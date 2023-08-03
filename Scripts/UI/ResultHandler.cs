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

    /*
    내부 데이터를 가져오고 외부 데이터로 내보낸다.
    그건 SaveNLoadManager을 통해서 가져오고 내보내면 될것 같다.

    마치 DataTrigger과 같지만, 데이터 트리거는 RunnerManager에 의존한다.
    따라서 이건 오직 SaveNLoadManager을 통해서 데이터 교환이 일어나겠다.
    */
    public List<TextRoulette> textRoulettes;

    private void Start()
    {
        bool IsClearState = true;
        if (SceneManager.GetActiveScene().name == "GameOverScene") { IsClearState = false; }
        float Score = GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.CollectedScore;
        if((int)Score == -1) {Score = 0;} 
        textRoulettes[(int)RESULT_INDEX.SCORE].CountingNumber = (int)Score;

        textRoulettes[(int)RESULT_INDEX.EXP].CountingNumber
           = (int)((IsClearState) ? (WinExpRatio * Score) : (LoseExpRatio * Score));

        textRoulettes[(int)RESULT_INDEX.COIN].CountingNumber
            = (int)((IsClearState) ? (WinCoinRatio * Score) : (LoseCoinRatio * Score));
    }
}
