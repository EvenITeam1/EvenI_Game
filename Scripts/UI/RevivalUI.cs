using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RevivalUI : MonoBehaviour
{
    public TextMeshProUGUI ContienueTMP;
    public TextMeshProUGUI TimmerTMP;
    public TextMeshProUGUI ButtonTMP;
    Coroutine CurrentCoroutine;
    private void OnEnable() {
        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = true;
        ContienueTMP.text = "Continue?";
        TimmerTMP.text = "10";
        ButtonTMP.text = $"부활하기 : <#F0F>{GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.RevivalCount}</color>";
    }

    public void DieEventHandler(){
        this.gameObject.SetActive(true);
        CurrentCoroutine = StartCoroutine(AsyncWaitingUntilLoadGameOver());
    }

    IEnumerator AsyncWaitingUntilLoadGameOver(){
        int waitCount = 10;
        while(waitCount > 0) {
            yield return new WaitForSecondsRealtime(1f);
            waitCount--;
            TimmerTMP.text = $"{waitCount}";
        }
        
        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;

        AsyncOperation op = SceneManager.LoadSceneAsync("GameOverScene");
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (op.progress < 0.9f)
        {
            yield return 0;
            timer += Time.unscaledDeltaTime;
            if(op.progress >= 0.9f) {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }

    public void HandleContinue(){
        if(GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.RevivalCount <= 0) return;
        StopCoroutine(CurrentCoroutine);
        RunnerManager.Instance.GlobalPlayer.Revival();
        this.gameObject.SetActive(false);
    }

    private void OnDisable() {
        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
    }
}