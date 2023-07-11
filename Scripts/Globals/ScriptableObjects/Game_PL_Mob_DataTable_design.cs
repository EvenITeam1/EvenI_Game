using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class Game_PL_Mob_DataTable_design : GoogleDataTable {
    public List<MobData> objectDataforms = new List<MobData>();
    public MobData emptyData = new MobData();
    
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1Dk64sbCeK46jXZKYK-rwXaaC1mfOYCLe_CpY7bFbm8A/export?format=tsv";

    [ContextMenu("구글 스프레드 시트 로딩")]
    public override async void LoadDataFromSheet(){
        await DownloadItemSO();
    }
    protected override async UniTask DownloadItemSO(){
        objectDataforms.Clear();

        objectDataforms.Add(emptyData);

        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;
        
        string[] lines = txt.Split('\n');
        for(int i = 5; i < lines.Length; i++){
            objectDataforms.Add( new MobData(lines[i]));
        }
    }

    public async UniTask<MobData> GetMobDataByINDEX(OBJECT_INDEX _index) {
        if(objectDataforms.Count == 0){await DownloadItemSO();}
        return objectDataforms[(int)((int)_index - MobData.indexBasis)];
    }
}