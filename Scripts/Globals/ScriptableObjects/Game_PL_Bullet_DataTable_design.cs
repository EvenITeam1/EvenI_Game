using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class Game_PL_Bullet_DataTable_design : MonoBehaviour {
    public List<ObjectData> objectDataforms = new List<ObjectData>();

    [ContextMenu("구글 스프레드 시트 로딩")]
    public void LoadDataFromSheet(){

    }
    //sasync UniTask DownloadItemSO(){
    //s}

    public ObjectData GetObjectDataByINDEX(OBJECT_INDEX _index) {
        //if(objectDataforms.Count == 0){await DownloadItemSO();}
        return objectDataforms[(int)((int)_index - ObjectData.indexBasis)];
    }
}