using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("2DRunner_Proto");
    }

    public void SceneChangeWithLoadingScene()
    {
        LoadingSceneManager.LoadScene("2DRunner_Proto");
    }
}
