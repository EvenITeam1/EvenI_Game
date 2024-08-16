using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CountDown : MonoBehaviour
{
    private void OnEnable()
    {
        ChangeSize().Forget();
    }

    async UniTaskVoid ChangeSize()
    {
        for (int i = 0; i < 3; i++)
        {
            transform.localScale = new Vector3(5, 5, 5);
            transform.DOScale(Vector3.one, 0.6f);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
        gameObject.SetActive(false);
    }
}
