using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class Game_PL_Bullet_DataTable_design : GoogleDataTable {
    public List<BulletData> bulletDataforms = new List<BulletData>();
    public BulletData emptyData = new BulletData();
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1fUBKqn2t9_q1L5WZ3k3FiO7ZKoH9YH_qROmQsmVt2bE/export?format=tsv";

    [ContextMenu("구글 스프레드 시트 로딩")]
    public override async void LoadDataFromSheet(){
        await DownloadItemSO();
    }

    protected override async UniTask DownloadItemSO(){
        bulletDataforms.Clear();

        bulletDataforms.Add(emptyData);

        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;
        
        string[] lines = txt.Split('\n');
        for(int i = 5; i < lines.Length; i++){
            bulletDataforms.Add( new BulletData(lines[i]));
        }
    }

    public async UniTask<BulletData> GetBulletDataByINDEX(BULLET_INDEX _index) {
        if(bulletDataforms.Count == 0){await DownloadItemSO();}
        return bulletDataforms[(int)((int)_index - BulletData.indexBasis)];
    }
}