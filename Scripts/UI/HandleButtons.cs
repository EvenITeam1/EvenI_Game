using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleButtons : MonoBehaviour
{
    public PauseMenu pauseMenu;
    private void Awake() {
    }
    public void HandleReturn(){
        pauseMenu.CloseMenu();
    }
    public void HandleRestart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void HandleQuit(){
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            // UnityEditor.EditorApplication.isPlaying = false;
            SceneManager.LoadScene("GameOverScene");
            RunnerManager.Instance.GlobalEventInstance.IsGamePaused = true;
        #else
            // Application.Quit();
            SceneManager.LoadScene("GameOverScene");
            RunnerManager.Instance.GlobalEventInstance.IsGamePaused = true;
        #endif
    }
}
