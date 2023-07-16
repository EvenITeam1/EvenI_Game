using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Game_PL_Boss_DataTable_design : GoogleDataTable
{
    public List<BossData> bossDataforms = new List<BossData>();
    public BossData emptyData = new BossData();
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1PTNd26FM7_YcQn40cekZiWj399LpfjikXxVS4PPjACg/export?format=tsv";
    

    [ContextMenu("구글 스프레드 시트 로딩")]
    public override async void LoadDataFromSheet()
    {
        await DownloadItemSO();
    }

    public override async UniTask DownloadItemSO()
    {
        bossDataforms.Clear();

        bossDataforms.Add(emptyData);

        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;

        string[] lines = txt.Split('\n');
        for (int i = 5; i < lines.Length; i++)
        {
            bossDataforms.Add(new BossData(lines[i]));
        }
    }

    public BossData GetBossDataByINDEX(BOSS_INDEX _index)
    {
        return bossDataforms[(int)_index - BossData.indexBasis];
    }
}
