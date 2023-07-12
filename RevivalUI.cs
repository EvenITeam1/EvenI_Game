using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RevivalUI : MonoBehaviour
{

    Coroutine CurrentCoroutine;
    private void OnEnable() {
        GameManager.Instance.GlobalEventInstance.IsGamePaused = true;    
    }

    public void DieEventHandler(){
        this.gameObject.SetActive(true);
        CurrentCoroutine = StartCoroutine(AsyncWaitingUntilLoadGameOver());
    }

    IEnumerator AsyncWaitingUntilLoadGameOver(){
        int waitCount = 10;
        while(waitCount > 0) {
            yield return YieldInstructionCache.WaitForSeconds(1f);
            waitCount--;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync("GameOverScene");
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (op.progress < 0.9f)
        {
            yield return null;
            timer += Time.deltaTime;
            if(op.progress >= 0.9f) {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }

    public void HandleContinue(){
        if(GameManager.Instance.GlobalSaveNLoad.saveData.outGameData.RevivalCount <= 0) return;
        StopCoroutine(CurrentCoroutine);
        GameManager.Instance.GlobalPlayer.Revival();
        this.gameObject.SetActive(false);
    }

    private void OnDisable() {
        GameManager.Instance.GlobalEventInstance.IsGamePaused = false;
    }
}
