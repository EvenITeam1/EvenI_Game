using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class TextBubble : MonoBehaviour
{
    public DOG_INDEX index;
    public TextMeshProUGUI bubbleText;

    [TextArea] public List<string> shibaText;
    [TextArea] public List<string> retriever1Text;
    [TextArea] public List<string> retriever2Text;
    [TextArea] public List<string> greyhoundText;
    [TextArea] public List<string> germanShepherdText;
    [TextArea] public List<string> huskyText;
    [TextArea] public List<string> wolfText;
    [TextArea] public List<string> pomeranian1Text;
    [TextArea] public List<string> pomeranian2Text;
    [TextArea] public List<string> pugText;
    List<string> textList;
    string selectedText;
    public float waitTime;

    private void OnEnable()
    {
        TextSelect();
        DogSay().Forget();
    }

    void TextSelect()
    {
        switch (index)
        {
            case DOG_INDEX.C_01:
                textList = shibaText;
                break;
            case DOG_INDEX.C_02:
                textList = retriever1Text;
                break;
            case DOG_INDEX.C_03:
                textList = retriever2Text;
                break;
            case DOG_INDEX.C_04:
                textList = greyhoundText;
                break;
            case DOG_INDEX.C_05:
                textList = germanShepherdText;
                break;
            case DOG_INDEX.C_06:
                textList = huskyText;
                break;
            case DOG_INDEX.C_07:
                textList = wolfText;
                break;
            case DOG_INDEX.C_08:
                textList = pomeranian1Text;
                break;
            case DOG_INDEX.C_09:
                textList = pomeranian2Text;
                break;
            case DOG_INDEX.C_10:
                textList = pugText;
                break;
            default:
                Debug.Log("지정된 캐릭터 인덱스가 올바르지 않음");
                break;
        }
        selectedText = textList[Random.Range(0, textList.Count)];
    }
    async UniTaskVoid DogSay()
    {
        for (int i = 0; i < selectedText.Length; i++)
        {
            bubbleText.text = selectedText.Substring(0, i + 1);
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        gameObject.SetActive(false);
    }


}
