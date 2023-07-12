using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class Game_PL_Object_DataTable_design : GoogleDataTable {
    public List<ObjectData> objectDataforms = new List<ObjectData>();
    public ObjectData emptyData = new ObjectData();
    
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1SQNTen-TlM7TX8ztqNLOEQKqp_E5UJgZkfHeCQUY768/export?format=tsv";

    [ContextMenu("구글 스프레드 시트 로딩")]
    public override async void LoadDataFromSheet(){
        await DownloadItemSO();
    }
    public override async UniTask DownloadItemSO(){
        objectDataforms.Clear();

        objectDataforms.Add(emptyData);

        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;
        
        string[] lines = txt.Split('\n');
        for(int i = 5; i < lines.Length; i++){
            objectDataforms.Add( new ObjectData(lines[i]));
        }
    }

    public ObjectData GetObjectDataByINDEX(OBJECT_INDEX _index) {
        return objectDataforms[(int)((int)_index - ObjectData.indexBasis)];
    }
}