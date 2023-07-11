using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadScript : MonoBehaviour
{
    public Animator Transition;
    public float TransitionTime;
    [ContextMenu("LoadNextLevel")]
    public void LoadNextLevel(){
        StartCoroutine(LoadLevel("TEMP_DEST_CutScene"));
    }

    IEnumerator LoadLevel(string _scene){
        Transition.SetTrigger("Start");
        
        yield return YieldInstructionCache.WaitForSeconds(TransitionTime);
        
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
