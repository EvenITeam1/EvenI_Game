[System.Serializable]
public class TipData
{
    public static readonly int indexBasis = 9000;
    public TIP_INDEX Index;
    public string tipString;

    public TipData()
    {
        this.Index = TIP_INDEX.DEFAULT;
        this.tipString = "empty";
    }

    public TipData(string _parsedLine)
    {
        string[] datas = _parsedLine.Trim().Split('\t');

        this.Index = (TIP_INDEX)int.Parse(datas[0]);
        string temp = datas[1].Replace('_', ' ');
        this.tipString = temp.Replace('n', '\n');
    }
}
