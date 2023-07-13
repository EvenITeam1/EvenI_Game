using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuObject;

    Queue<GameObject> menuQueue = new Queue<GameObject>();

    public void OpenMenu(){
        menuQueue.Enqueue(pauseMenuObject);
        var topMenu = menuQueue.Peek();
        topMenu.SetActive(true);
        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = true;
    }

    public void CloseMenu(){
        var topMenu = menuQueue.Peek();
        topMenu.SetActive(false);
        menuQueue.Dequeue();
        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(menuQueue.Count == 0){
                OpenMenu(); 
            }
            else if(menuQueue.Count == 1){ 
                CloseMenu();
            }
        }
    }
}
