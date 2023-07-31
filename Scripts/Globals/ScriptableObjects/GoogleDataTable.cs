using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class GoogleDataTable : MonoBehaviour{
    const string sheet_URL = "";
    protected virtual void Awake(){}
    public virtual async void LoadDataFromSheet(){ await UniTask.Delay(TimeSpan.FromSeconds(0.01f)); }
    public virtual async UniTask DownloadItemSO(){await UniTask.Delay(TimeSpan.FromSeconds(0.01f));}
    public virtual void AfterDownloadItemSO(){}
}