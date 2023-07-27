using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour, IPointer​Click​Handler
{
    [TextArea]
    public List<string> cutSceneTextList = new List<string>();
    
    [Tooltip("1 _개행문자_ 실제텍스트 입력하자 , 1 = 주인공 2 = 요정")]
    public TextMeshProUGUI[] talkTextMeshProGUI;
    public int count = 0;
    private bool  isClickable = true;
    private void ReActivateClick(){isClickable = true;}

    public string LoadSceneString;

    private void Start() {
        StartCoroutine(LoadLevel(LoadSceneString));
        if(cutSceneTextList.Count >= 1)HandleTalk(cutSceneTextList[count++]);
    }

    void HandleTalk(string _str)
    {
        string[] lines = _str.Split('\n');
        if (lines.Length <= 1) { throw new System.Exception("형식이 안맞는 대회임"); }
        string showTextStr = "";
        
        for (int i = 1; i < lines.Length; i++)
        {
            showTextStr += $"{lines[i]}\n";
        }

        switch (int.Parse(lines[0]))
        {
            case 1:
                {
                    talkTextMeshProGUI[0].text = $"나 : {showTextStr}\n";
                    break;
                }
            case 2:
                {
                    talkTextMeshProGUI[1].text = $"OOO : <#FF7B81>{showTextStr}</color>\n";
                    break;
                }
            default:
                {
                    throw new System.Exception("숫자가 잘못된듯 1 or 2로 ㄱㄱ");
                }
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(isClickable != true) {return;}
        isClickable = false; Invoke("ReActivateClick", 0.3f);

        if (count >= cutSceneTextList.Count)
        {
            count++;
            return;
        }
        else
        {
            HandleTalk(cutSceneTextList[count++]);
        }
    }

    IEnumerator LoadLevel(string _scene)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_scene);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            yield return null;
            if(op.progress >= 0.9f && count >= cutSceneTextList.Count + 1) {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
