using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoadScript : MonoBehaviour
{
    public Animator Transition;
    public bool IsTransitionEnable = true;
    public string   scene;
    public float TransitionTime;

    // 바로아래 Crossfade의 자식으로 들어가 있는 Plane의 SetActive(true)
    // 러너
        //DataTrigger.cs LoadOutToInGame 연결 되어있느지
    // 보스
        //DataTrigger.cs LoadRunnerToBossGame 연결이 되어 있는지
    public UnityEvent InvokeLevelEvent;

    public UnityEvent ExitLevelEvent;

    private int currentStageNumber;

    private void Awake() {
        currentStageNumber = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CurrentStageNumber;
    }

    private void OnEnable() {
        InitializeCurrentLevel();
    }

    [ContextMenu("InitializeCurrentLevel")]
    public void InitializeCurrentLevel(){
        InvokeLevelEvent.AddListener(() => {GameManager.Instance.GlobalSoundManager.FadeInBGM(1);});
        InvokeLevelEvent.AddListener(() => {GameManager.Instance.GlobalSoundManager.FadeInSFX(1);});
        if(SceneManager.GetActiveScene().name.Contains("Raid")){
            InvokeLevelEvent.AddListener(() => {GameManager.Instance.GlobalSoundManager.PlayBGMByString("BGM_BossRaid");});
        }

        else if(SceneManager.GetActiveScene().name.Contains("BossStage"))
        {
            InvokeLevelEvent.AddListener(() => { GameManager.Instance.GlobalSoundManager.PlayBGMByString("BGM_Boss" + currentStageNumber.ToString()); });
        }

        else 
        {
            InvokeLevelEvent.AddListener(() => {GameManager.Instance.GlobalSoundManager.PlayBGMByString("BGM_Stage" + currentStageNumber.ToString());});
        }
        ExitLevelEvent.AddListener(() => {GameManager.Instance.GlobalSoundManager.FadeOutBGM(1);});
        ExitLevelEvent.AddListener(() => {GameManager.Instance.GlobalSoundManager.FadeOutSFX(1);});
    }

    private void Start() {
        StartCoroutine(InvokeLevel());
    }

    IEnumerator InvokeLevel() {
        InvokeLevelEvent.Invoke();
        yield break;
    }


    IEnumerator ExitLevel(){
        ExitLevelEvent.Invoke();
        if(IsTransitionEnable) Transition.SetTrigger("Start");
        yield return YieldInstructionCache.WaitForSeconds(TransitionTime);
    }

    [ContextMenu("LoadNextLevel")]
    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(scene));
    }
    public void LoadNextLevelForce(){
        SceneManager.LoadScene(scene);
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
        op.allowSceneActivation = true;
    }
}
