using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Newtonsoft.Json;
using System.IO;
using System.Text;


public class StartSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] LoopType _loopType;
    float time;

    public bool IsLoadedOnce = false;

    void Start()
    {
        time = 0;
        _text.DOFade(0.0f, 1).SetLoops(-1, _loopType);
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 2.5f)
        {
            if (Input.anyKeyDown && !IsLoadedOnce)
            {
                if(!File.Exists(string.Format("{0}/{1}.json", Application.persistentDataPath, "NickNameJsonData"))){
                    IsLoadedOnce= true;
                    LoadingSceneManager.LoadScene("StageOpening");
                }
                else {
                    IsLoadedOnce= true;
                    LoadingSceneManager.LoadScene("LobbyScene");
                }
            }
        }      
    }
}
