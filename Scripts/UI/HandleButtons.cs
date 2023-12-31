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
            
            RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
            // Button에 DataTrigger.SaveGameOver()가 이벤트 바인딩 되어 있는지 잘 확인하자.
            GameManager.Instance.GlobalSoundManager.StopBGM();
            SceneManager.LoadScene("GameOverScene");
        #else
            // Application.Quit();
            RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
            // Button에 DataTrigger.SaveGameOver()가 이벤트 바인딩 되어 있는지 잘 확인하자.
            GameManager.Instance.GlobalSoundManager.StopBGM();
            SceneManager.LoadScene("GameOverScene");
        #endif
    }
}
