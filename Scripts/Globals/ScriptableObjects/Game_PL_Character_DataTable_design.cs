using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class Game_PL_Character_DataTable_design : MonoBehaviour {
    public List<PlayerData> playerDataforms = new List<PlayerData>();
    public PlayerData emptyData = new PlayerData();
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1PgCd0ur1vz6mA0GTe5XthIgAzPK8fso36iuDjJ_5HcI/export?format=tsv";

    private void Awake() {
    }
    [ContextMenu("구글 스프레드 시트 로딩")]
    public async void LoadDataFromSheet(){
        await DownloadItemSO();
    }

    async UniTask DownloadItemSO(){
        playerDataforms.Clear();
        playerDataforms.Add(emptyData);
        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;
        string[] lines = txt.Split('\n');
        for(int i = 4; i < lines.Length; i++){
            playerDataforms.Add(new PlayerData(lines[i]));
        }
    }

    public async UniTask<PlayerData> GetPlayerDataByINDEX(DOG_INDEX _index) {
        if(playerDataforms.Count == 0){await DownloadItemSO();}
        return playerDataforms[(int)((int)_index - PlayerData.indexBasis)];
    }
}