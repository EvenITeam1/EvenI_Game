using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoadScript : MonoBehaviour
{
    public Animator Transition;
    public string scene;
    public float TransitionTime;

    // �ٷξƷ� Crossfade�� �ڽ����� �� �ִ� Plane�� SetActive(true)
    // ����
    //DataTrigger.cs LoadOutToInGame ���� �Ǿ��ִ���
    // ����
    //DataTrigger.cs LoadRunnerToBossGame ������ �Ǿ� �ִ���
    public UnityEvent InvokeLevelEvent;

    //
    public UnityEvent ExitLevelEvent;

    private void Awake()
    {

    }
    private void OnEnable()
    {
        InitializeCurrentLevel();
    }
    IEnumerator InvokeLevel()
    {
        InvokeLevelEvent.Invoke();
        yield break;
    }

    [ContextMenu("InitializeCurrentLevel")]
    public void InitializeCurrentLevel()
    {
        InvokeLevelEvent.AddListener(() => { GameManager.Instance.GlobalSoundManager.FadeInBGM(1); });
        ExitLevelEvent.AddListener(() => { GameManager.Instance.GlobalSoundManager.FadeOutBGM(1); });
    }

    private void Start()
    {
        StartCoroutine(InvokeLevel());
    }

    IEnumerator ExitLevel()
    {
        ExitLevelEvent.Invoke();
        Transition.SetTrigger("Start");
        yield return YieldInstructionCache.WaitForSeconds(TransitionTime);
    }

    [ContextMenu("LoadNextLevel")]
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(scene));
    }
    public void LoadNextLevelForce()
    {
        SceneManager.LoadScene(scene);
    }

    IEnumerator LoadLevel(string _scene)
    {
        yield return StartCoroutine(ExitLevel());

        AsyncOperation op = SceneManager.LoadSceneAsync(_scene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (op.progress < 0.9f)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}