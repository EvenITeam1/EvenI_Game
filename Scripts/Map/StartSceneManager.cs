using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class StartSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] LoopType _loopType;
    float time;

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
            if (Input.anyKeyDown)
            {
                LoadingSceneManager.LoadScene("LobbyScene");
            }
        }      
    }
}
