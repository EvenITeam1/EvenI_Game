using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoadScript : MonoBehaviour
{
    public Animator Transition;
    public string   scene;
    public float TransitionTime;

    public UnityEvent InvokeLevelEvent;
    public UnityEvent ExitLevelEvent;

    private void Awake() {
        InitializeCurrentLevel();
    }
    IEnumerator InvokeLevel() {
        InvokeLevelEvent.Invoke();
        yield break;
    }

    [ContextMenu("InitializeCurrentLevel")]
    public void InitializeCurrentLevel(){
        StartCoroutine(InvokeLevel());
    }

    IEnumerator ExitLevel(){
        ExitLevelEvent.Invoke();
        Transition.SetTrigger("Start");
        yield return YieldInstructionCache.WaitForSeconds(TransitionTime);
    }

    [ContextMenu("LoadNextLevel")]
    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string _scene){
        yield return StartCoroutine(ExitLevel());

        AsyncOperation op = SceneManager.LoadSceneAsync(_scene);
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
}
