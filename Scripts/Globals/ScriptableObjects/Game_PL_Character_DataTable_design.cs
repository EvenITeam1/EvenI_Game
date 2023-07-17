using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class Game_PL_Character_DataTable_design : GoogleDataTable {
    public List<PlayerData> playerDataforms = new List<PlayerData>();
    [Tooltip("이 테이블과 관련된 모든 프리펩을 추가해야함")]
    public List<Player> Player_Prefebs = new List<Player>();
    public PlayerData emptyData = new PlayerData();
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1GiyKuTkw0z-Sp0Jo_CU6DcVtq8L0MNkyqI82eVjl_5Y/export?format=tsv";

    [ContextMenu("구글 스프레드 시트 로딩")]
    public override async void LoadDataFromSheet(){
        await DownloadItemSO();
    }

    public override async UniTask DownloadItemSO(){
        playerDataforms.Clear();

        playerDataforms.Add(emptyData);

        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;
        
        string[] lines = txt.Split('\n');
        for(int i = 5; i < lines.Length; i++){
            playerDataforms.Add( new PlayerData(lines[i]));
            Player_Prefebs[i - 5].playerData = playerDataforms.Last();
        }
    }

    public PlayerData GetPlayerDataByINDEX(DOG_INDEX _index) {
        return playerDataforms[(int)((int)_index - PlayerData.indexBasis)];
    }
}