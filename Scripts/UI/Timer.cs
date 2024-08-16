using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float time { get; private set; }
    [SerializeField] TextMeshProUGUI timerText;

    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timerText.text = $"{(int)time/60}Ка {(int)time%60}УЪ";
    }
}
