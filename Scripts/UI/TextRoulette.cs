using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;

public class TextRoulette : MonoBehaviour{
    public TextMeshProUGUI TargetTextMeshPro;
    public string BaseString;
    public float CountSpeed = 1f;
    private int countingNumber = 0;
    public int CountingNumber {
        get {return countingNumber;}
        set {countingNumber = value;}
    }

    private void Awake() {
        TargetTextMeshPro = GetComponent<TextMeshProUGUI>();
        BaseString = TargetTextMeshPro.text;
    }

    private void OnEnable() {
        CountEffect();
    }

    [ContextMenu("드르륵 거리는 숫자 이펙트")]
    [ExecuteInEditMode]
    
    public void TEST_CountEffect(){
        CountingNumber = 1234;
    }

    private void CountEffect(){
        DOVirtual.Int(0, CountingNumber, CountSpeed, (E) => {TargetTextMeshPro.text = BaseString + E.ToString();});
    }

    private void CountAdd(int _addingNum) {
        int destNum = countingNumber + _addingNum;
        DOVirtual.Int(countingNumber, destNum, CountSpeed, (E) => {TargetTextMeshPro.text = BaseString + E.ToString();});
    }
}