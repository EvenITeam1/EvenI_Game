using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class Game_PL_Mob_DataTable_design : GoogleDataTable {
    public List<MobData> mobDataforms = new List<MobData>();
    [Tooltip("이 테이블과 관련된 모든 프리펩을 추가해야함")]
    public List<Mob> Mob_Prefebs = new List<Mob>();
    public MobData emptyData = new MobData();
    
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1Dk64sbCeK46jXZKYK-rwXaaC1mfOYCLe_CpY7bFbm8A/export?format=tsv";

    [ContextMenu("구글 스프레드 시트 로딩")]
    public override async void LoadDataFromSheet(){
        await DownloadItemSO();
    }
    public override async UniTask DownloadItemSO(){
        mobDataforms.Clear();

        mobDataforms.Add(emptyData);

        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;
        
        string[] lines = txt.Split('\n');
        // 앞으로 ForEach는 테이블이 아닌 프리펩의 개수만큼 불러온다
        // 이유는 테이블 상황이 현재 너무 불안정하므로, 프리펩의 수 기준으로 받아오는것이 옳다 판단
        int lineStart = 5;
        for(int i = 0; i < Mob_Prefebs.Count; i++){
            mobDataforms.Add( new MobData(lines[lineStart + i]));
            Mob_Prefebs[i].mobData = mobDataforms.Last();
        }
    }

    public MobData GetMobDataByINDEX(MOB_INDEX _index) {
        return mobDataforms[(int)((int)_index - MobData.indexBasis)];
    }
}