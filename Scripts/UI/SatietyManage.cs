using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class SatietyManage : MonoBehaviour
{
    [SerializeField] List<Image> guage;
    [SerializeField] Color fillColor;
    [SerializeField] TextMeshProUGUI guageCountText;
    [SerializeField] TextMeshProUGUI leftTimeText;
    [SerializeField] TextAsset JsonFile;
    static bool isFirstAccess = true;
    public static int chargeCount;
    public float initialChargeTimeBySec;
    float chargeTimeBySec;
    public float remainTime;
    float timeLeft;
    static float passedTimeInLobby;
    public static DateTime prevTime = DateTime.MaxValue;

    private void Awake()
    {
        if(isFirstAccess)
        {
            prevTime = JsonConvert.DeserializeObject<DateTime>(JsonFile.text);
            isFirstAccess = false;
        }    
    }
    private void Start()
    {
        CalculateCountOutside();
        remainTime = 1;
        chargeTimeBySec = initialChargeTimeBySec - timeLeft + Time.time;
        refreshGuageColor();
        refreshCountText();
    }
    private void Update()
    {
       CalculateCountInside();
    }
    public static void RecordLobbyOutTime()//for Outside
    {
        prevTime = DateTime.Now;
    }

    public void CalculateCountInside()
    {
        if (isFull()) { }

        else if (remainTime > 0)
        {
            remainTime = chargeTimeBySec - Time.time;
            passedTimeInLobby = initialChargeTimeBySec - remainTime;
            int leftMin = (int)remainTime / 60;
            int leftSec = (int)remainTime % 60;
            leftTimeText.text = $"{leftMin} : {leftSec}";
        }
           
        else
        {
            chargeCount++;
            refreshGuageColor();
            refreshCountText();
            remainTime = 1;
            chargeTimeBySec = initialChargeTimeBySec + Time.time;
        }
    }
    public void CalculateCountOutside()
    {
        if (isFull()) {}

        else
        {
            TimeSpan dt = DateTime.Now - prevTime;
            if(dt.TotalHours < 0) {return;}
            int plusCount = (int)(dt.TotalSeconds + passedTimeInLobby) / (int)initialChargeTimeBySec;
            timeLeft = (int)(dt.TotalSeconds + passedTimeInLobby) % (int)initialChargeTimeBySec;
            chargeCount += plusCount;
            if (chargeCount > 10)
                chargeCount = 10;
        }      
    }

    public void refreshGuageColor()
    {
        for (int i = 0; i < chargeCount; i++)        
            guage[i].color = fillColor;

        for (int i = chargeCount; i <10; i++)
        {
            guage[i].color = Color.white;
        }
    }

    public void refreshCountText()
    {
        guageCountText.text = chargeCount + "/10";
    }

    public bool isFull()
    {
        if (chargeCount == 10)
            return true;

        else
            return false;
    }

}
