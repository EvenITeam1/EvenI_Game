using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class StartSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] LoopType _loopType;

    void Start()
    {
        _text.DOFade(0.0f, 1).SetLoops(-1, _loopType);
    }
    void Update()
    {
        if(Input.anyKeyDown)
        {
            LoadingSceneManager.LoadScene("LobbyScene");
        }
    }


}
