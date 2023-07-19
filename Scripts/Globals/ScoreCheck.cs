using UnityEngine;
using UnityEngine.UI;

public class ScoreCheck : MonoBehaviour {
    private float mScore = 0;
    public Text ScoreUI;
    public float Score{
        get {return mScore;}
        set {
            mScore = value;
            ScoreUI.text = $"Score : {(int)mScore}";
        }
    }

    private void Start() {
        ScoreUI ??= GameObject.Find("ScoreUI").GetComponent<Text>();
    }
}