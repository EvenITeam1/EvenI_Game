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
        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
        GameManager.Instance.GlobalSoundManager.StopBGM();
        GameManager.Instance.GlobalSoundManager.StopSFX();
        SceneManager.LoadScene("GameOverScene");
    }
}
