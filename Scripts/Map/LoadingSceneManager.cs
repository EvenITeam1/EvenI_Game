using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Slider progressBar;
    [SerializeField] GameObject tipObj;
    [SerializeField] float timeMultiplier;//0.2f is basic amount
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] TipData tipData;
    [SerializeField] TIP_INDEX finalIndex;

    private void Start()
    {
        progressBar.value = 0f;
        getRandomTipText();
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer * timeMultiplier);
                if (progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        yield return new WaitUntil(() => {return GameManager.Instance.IsAsyncLoaded;});
    }
    void getRandomTipText()
    {
        int n = Random.Range((int)TIP_INDEX.TIP_9001, (int)finalIndex + 1);
        tipData = GameManager.Instance.TipDataTableDesign.GetTipDataByINDEX((TIP_INDEX)n);
        loadingText.text = "Tip. " + tipData.tipString;
    }
}
