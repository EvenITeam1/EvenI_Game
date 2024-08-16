using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class LoadRankData : GoogleDataTable
{
    public List<RankData> rankDataforms = new List<RankData>();
    public List<RankData> sorted = new List<RankData>();
    public List<RankVisualizeData> visualDatas;
    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1fG_LsyHmABqyPubzwpf03itYSjKvliXrIDXGsWe1LVY/export?format=tsv";


    new private void Awake()
    {
        LoadDataFromSheet();
    }

    [ContextMenu("구글 스프레드 시트 로딩")]
    public override async void LoadDataFromSheet()
    {
        await DownloadItemSO();
    }

    public override async UniTask DownloadItemSO()
    {
        rankDataforms.Clear();
        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;

        string[] lines = txt.Split('\n');

        int lineStart = 1;
        for (int i = 0; i < lines.Length - 1; i++)
        {
            rankDataforms.Add(new RankData(lines[lineStart + i]));
        }

        sorted = rankDataforms.OrderBy(x => x.score)
                             .ThenByDescending(x => x.timeMin).ThenByDescending(x => x.timeSec)
                             .ToList();

        putDataInRankBoard();
    }

    public void putDataInRankBoard()
    {
        int n = sorted.Count - 1;
        if (sorted.Count < 20)
        {
            for (int i = 0; i < sorted.Count; i++)
            {
                visualDatas[i].nickname.text = sorted[n - i].nickname;
                visualDatas[i].character.text = sorted[n - i].character;
                visualDatas[i].score.text = sorted[n - i].score.ToString();
                visualDatas[i].time.text = $"{sorted[n - i].timeMin}분 {sorted[n - i].timeSec}초";
            }

            for (int i = sorted.Count; i < 20; i++)
            {
                visualDatas[i].nickname.text = "-";
                visualDatas[i].character.text = "-";
                visualDatas[i].score.text = "-";
                visualDatas[i].time.text = "00분 00.000초";
            }
        }

        else
        {
            for (int i = 0; i < 20; i++)
            {
                visualDatas[i].nickname.text = sorted[n - i].nickname;
                visualDatas[i].character.text = sorted[n - i].character;
                visualDatas[i].score.text = sorted[n - i].score.ToString();
                visualDatas[i].time.text = $"{sorted[n - i].timeMin}분 {sorted[n - i].timeSec}초";
            }
        }
    }






    public override void AfterDownloadItemSO()
    {
        return;
    }
}
