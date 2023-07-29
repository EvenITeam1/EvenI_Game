using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreCheck : MonoBehaviour {
    private float mScore = 0;
    public TextMeshProUGUI ScoreUI;
    public float Score{
        get {return mScore;}
        set {
            mScore = value;
            ScoreUI.text = $"Score : {(int)mScore}";
        }
    }

    private void Start() {
        ScoreUI ??= GameObject.Find("ScoreUI").GetComponent<TextMeshProUGUI>();
    }
}