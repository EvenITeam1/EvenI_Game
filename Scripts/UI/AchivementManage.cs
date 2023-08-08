using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchivementManage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI characterCount;
    [SerializeField] TextMeshProUGUI stageClearCount;
    int cnt = 0;
    void Start()
    {
        SetCharacterCount();
        characterCount.text = $"보유한 캐릭터 : {cnt} / 10 ({cnt * 10}%)";
        int cnt2 = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked;
        stageClearCount.text = $"클리어한 스테이지 : {cnt2} / 10 ({cnt2 * 10}%)";
    }

    void SetCharacterCount()
    {
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isShibaUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenRetrieverUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isLabradorRetrieverUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGreyHoundUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGermanShepherdUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isHuskyUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWolfUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isGoldenPomeranianUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isWhitePomeranianUnlocked)
            cnt++;
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.isPugUnlocked)
            cnt++;
    }
}
