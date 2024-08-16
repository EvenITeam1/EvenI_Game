using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject quitPopUpCanvas;
    [SerializeField] FreeItemManage freeItemManageScript;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Escape))
        {
            switch(sceneName)
            {
                case "LobbyScene": quitPopUpCanvas.SetActive(true);
                    break;

                case "ShopScene": freeItemManageScript.SaveFreeItemDataToJson();
                    SceneManager.LoadScene("LobbyScene");
                    break;

                default: SceneManager.LoadScene("LobbyScene");
                    break;
            }
        }
    }

    public void QuitGame()
    {
        SatietyManage.SaveSatietyDataToJson();
        Application.Quit();      
    }
}
