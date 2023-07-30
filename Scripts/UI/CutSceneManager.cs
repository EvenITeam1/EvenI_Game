using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class CutSceneTextData {
    public int CST_stageNumber;
    public int CST_phase;
    public string CST_talkingOwner;
    public string CST_text;
    public Color CST_contentColor; //HexColor 값으로 넣기
    public int CST_alignmentType; // -1, 0 ,1 왼, 중, 오

    public CutSceneTextData() {
        this.CST_stageNumber    = -1;
        this.CST_phase          = -1;
        this.CST_talkingOwner      = "이름";
        this.CST_text           = "내용";
    }

    public CutSceneTextData(string _parsedLine) 
    {
        string[] datas = _parsedLine.Trim().Split('\t');
        // this.CST_Index       = (TEXT_INDEX)int.Parse(dats[0]); //굳이 인덱스 필요할까?
        this.CST_stageNumber    = int.Parse(datas[1]);
        this.CST_phase          = int.Parse(datas[2]);
        this.CST_talkingOwner      = datas[3].Replace('_', ' ');
        this.CST_text           = datas[4].Replace('_', ' ');
        ColorUtility.TryParseHtmlString(datas[5], out this.CST_contentColor);
    }

    public override string ToString() {
        return $"{CST_stageNumber} {CST_phase} {CST_talkingOwner} {CST_text.Replace(' ', '_')} {CST_contentColor} {CST_alignmentType}";
    }
}

public class CutSceneManager : MonoBehaviour, IPointer​Click​Handler
{
    [Tooltip("1 = 주인공 2 = 요정")]
    public List<CutSceneTextData> cutSceneTextDatas = new List<CutSceneTextData>();

    public TextMeshProUGUI nameTextMeshProGUI;
    public TextMeshProUGUI contentTextMeshProGUI;

    public GameObject      clickAlertUI;

    public string LoadSceneString;
    public int count = 0;

    const string sheet_URL = "https://docs.google.com/spreadsheets/d/1yO2F-VKl9_f7EDcuLi8hvxDhdEv3qewkVDISBV5OYmE/edit?usp=sharing/export?format=tsv";
    private bool isClickable =false;
    public void ActivateClick(){
        isClickable = true;
        clickAlertUI.SetActive(true);
    }
    public void DeactivateClick(){
        isClickable = false;
        clickAlertUI.SetActive(false);
    }
 

    private void Start() {
        StartCoroutine(LoadScene(LoadSceneString));
        Invoke("ActivateClick", 1f);
        if(cutSceneTextDatas.Count >= 1) HandleTalk(cutSceneTextDatas[count++]);
    }
    

    void HandleTalk(CutSceneTextData _cutSceneTextData)
    {
        nameTextMeshProGUI.text = "";
        contentTextMeshProGUI.text = "";
        // SetName
        nameTextMeshProGUI.text     = _cutSceneTextData.CST_talkingOwner;

        // SetContent
        contentTextMeshProGUI.text  = _cutSceneTextData.CST_text;

        // SetColor
        contentTextMeshProGUI.color = _cutSceneTextData.CST_contentColor;

        // SetAlignmentOptions
        switch (_cutSceneTextData.CST_alignmentType) {
            case -1: {
                nameTextMeshProGUI.alignment = TextAlignmentOptions.TopLeft; 
                contentTextMeshProGUI.alignment = TextAlignmentOptions.TopLeft;
                break;
            }
            case 0 : {
                nameTextMeshProGUI.alignment = TextAlignmentOptions.Top;
                contentTextMeshProGUI.alignment = TextAlignmentOptions.Top;
                break;
            }
            case 1 : {
                nameTextMeshProGUI.alignment = TextAlignmentOptions.TopRight;
                contentTextMeshProGUI.alignment = TextAlignmentOptions.TopRight;
                break;
            }
            default : 
                throw new System.Exception("-1, 0, 1 중 하나로 불러와주세요");
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(isClickable != true) {return;}
        DeactivateClick(); Invoke("ActivateClick", 0.3f);

        if (count >= cutSceneTextDatas.Count)
        {
            count++;
            return;
        }
        else
        {
            HandleTalk(cutSceneTextDatas[count++]);
        }
    }
     IEnumerator LoadScene(string _scene)
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(_scene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            Debug.Log(op.progress);
            if(op.progress >= 0.9f)
            {
                Debug.Log("End");
                break;
            }
            yield return null;
        }
        yield return new WaitUntil(() => {return count >= cutSceneTextDatas.Count+1;});
        op.allowSceneActivation = true;
    }

    [ContextMenu("컷씬 스크립트 수동으로 불러오기")]
    public async void LoadDataFromSheet()
    {
        await DownloadItemSO();
    }

    [ContextMenu("Print CST리스트 ToString")]
    public void PrintCTSs(){
        string retString = "";
        cutSceneTextDatas.ForEach(E => {retString += E.ToString() + '\n';});
        Debug.Log(retString);
    }

    public async UniTask DownloadItemSO()
    {
        var txt = (await UnityWebRequest.Get(sheet_URL).SendWebRequest()).downloadHandler.text;
        string[] lines = txt.Split('\n');
        int lineStart = 5;

        cutSceneTextDatas.Clear();

        for (int i = lineStart; i < lines.Length; i++)
        {
            cutSceneTextDatas.Add(new CutSceneTextData(lines[i]));
        }
    }
}