using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class Game_PL_Tip_DataTable_design : GoogleDataTable
{
    public List<TipData> tipDataforms = new List<TipData>();
    public TipData emptyData = new TipData();
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1niYGCPz7sVuMbFKGgYGfjReNatAX2sZJ7jFdW24xsW0/export?format=tsv";
    [ContextMenu("구글 스프레드 시트 로딩")]
    public override async void LoadDataFromSheet()
    {
        await DownloadItemSO();
    }

    public override async UniTask DownloadItemSO()
    {
        tipDataforms.Clear();

        tipDataforms.Add(emptyData);

        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;

        string[] lines = txt.Split('\n');
        for (int i = 5; i < lines.Length; i++)
        {
            tipDataforms.Add(new TipData(lines[i]));
        }
    }

    public TipData GetTipDataByINDEX(TIP_INDEX _index)
    {
        return tipDataforms[(int)((int)_index - TipData.indexBasis)];
    }
}
