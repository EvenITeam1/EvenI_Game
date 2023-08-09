using TMPro;
[System.Serializable]
public class RankData
{
    public string nickname;
    public string character;
    public int score;
    public int timeMin;
    public float timeSec;

    public RankData()
    {
        nickname = "empty";
        character = "emptyDog";
        score = -1;
        timeMin = -1;
        timeSec = -1;
    }

    public RankData(string _parsedLine)
    {
        string[] datas = _parsedLine.Trim().Split('\t');

        nickname = datas[0];
        character = datas[1];
        score = int.Parse(datas[2]); 
        timeMin = int.Parse(datas[3]);
        timeSec = float.Parse(datas[4]);
    }
}

[System.Serializable]
public class RankVisualizeData
{
    public TextMeshProUGUI nickname;
    public TextMeshProUGUI character;
    public TextMeshProUGUI score; 
    public TextMeshProUGUI time;
}
