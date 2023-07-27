using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;
public class TutorialUI : MonoBehaviour
{
    [HideInInspector]public Animator paperAnimator;
    [HideInInspector]public Animator tutorialAnimator;
    [HideInInspector]public Animator textAnimator;
    
    public TextMeshProUGUI messageTextMeshPro;

    [ContextMenu("TextMeshPro 연결")]
    private void ConnectTextMeshProUGUIComponent(){
        messageTextMeshPro = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void SetMessageString(string _str){
        messageTextMeshPro = GetComponentInChildren<TextMeshProUGUI>(true);
        messageTextMeshPro.text = _str; 
    }
}
