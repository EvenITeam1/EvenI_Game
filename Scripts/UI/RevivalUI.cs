using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RevivalUI : MonoBehaviour
{
    public UnityEvent OnReveival;
    public UnityEvent OnDieEvent;

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

    public void HandleQuit(){
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            // UnityEditor.EditorApplication.isPlaying = false;
            
            RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
            // Button에 DataTrigger.SaveGameOver()가 이벤트 바인딩 되어 있는지 잘 확인하자.
            SceneManager.LoadScene("GameOverScene");
            GameManager.Instance.GlobalSoundManager.StopBGM();
        #else
            // Application.Quit();
            RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
            // Button에 DataTrigger.SaveGameOver()가 이벤트 바인딩 되어 있는지 잘 확인하자.
            GameManager.Instance.GlobalSoundManager.StopBGM();
            SceneManager.LoadScene("GameOverScene");
        #endif
    }

    IEnumerator AsyncWaitingUntilLoadGameOver(){
        int waitCount = 10;
        while(waitCount > 0) {
            yield return new WaitForSecondsRealtime(1f);
            waitCount--;
            TimmerTMP.text = $"{waitCount}";
        }

        OnDieEvent.Invoke();

        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
        AsyncOperation op = SceneManager.LoadSceneAsync("GameOverScene");
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            if(op.progress >= 0.9f) {
                op.allowSceneActivation = true;
                break;
            }
        }
        op.allowSceneActivation = true;
    }

    public void HandleContinue(){
        if(GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.RevivalCount <= 0) return;
        StopCoroutine(CurrentCoroutine);
        OnReveival.Invoke();
        RunnerManager.Instance.GlobalPlayer.Revival();
        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
        this.gameObject.SetActive(false);
    }
}