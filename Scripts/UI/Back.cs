using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Back : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject quitPopUpCanvas;
    DateTime quitTime;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            switch(sceneName)
            {
                case "LobbyScene": quitPopUpCanvas.SetActive(true);
                    break;

                default: SceneManager.LoadScene("LobbyScene");
                    break;
            }
        }
    }

    public void QuitGame()
    {
        saveQuitTimeToJson();
        Application.Quit();
    }

    void saveQuitTimeToJson()
    {
        quitTime = DateTime.Now;
        var result = JsonConvert.SerializeObject(quitTime);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.txt", Application.dataPath, "QuitDateTime"), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(result);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
}
