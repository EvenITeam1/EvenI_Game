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
    
    private TextMeshProUGUI messageTextMeshPro;
    public void SetMessageString(string _str){ messageTextMeshPro.text = _str; }

    private void OnEnable() {
        messageTextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }
}
