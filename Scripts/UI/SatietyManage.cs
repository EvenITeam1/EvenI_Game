using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class SatietyManage : MonoBehaviour
{
    [SerializeField] List<Image> guage;
    [SerializeField] Color fillColor;
    [SerializeField] TextMeshProUGUI guageCountText;
    [SerializeField] TextMeshProUGUI leftTimeText;
    private static int chargeCount;
    public float initialChargeTimeBySec;
    float chargeTimeBySec;
    public float remainTime;
    float timeLeft;
    private static float passedTimeInLobby;
    public static DateTime prevTime = DateTime.MaxValue;

    private void Awake()
    {
        LoadSatietyDataFromJson();
        refreshGuageColor();
        refreshCountText();
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

    public static void SaveSatietyDataToJson()
    {
        SatietyJsonData satieTyJsonData = new SatietyJsonData(DateTime.Now, chargeCount, passedTimeInLobby);
        var result = JsonConvert.SerializeObject(satieTyJsonData);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.persistentDataPath, "QuitTimeData"), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(result);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void LoadSatietyDataFromJson()
    {
        if (!File.Exists(string.Format("{0}/{1}.json", Application.persistentDataPath, "QuitTimeData")))
        {
            Debug.Log("앱 최초실행 : 행동력 스크립트");
            chargeCount = 10;
        }

        else
        {
            string JsonFileText = File.ReadAllText(string.Format("{0}/{1}.json", Application.persistentDataPath, "QuitTimeData"));
            SatietyJsonData satietyJsonData = JsonConvert.DeserializeObject<SatietyJsonData>(JsonFileText);
            prevTime = satietyJsonData.quitTime;
            chargeCount = satietyJsonData.chargeCount;
            passedTimeInLobby = satietyJsonData.passedTimeInLobby;
        }   
    }

    public static int GetChargeCount()
    {
        return chargeCount;
    }

    public static void UseChargeCount(int n)
    {
        if(chargeCount < n)
        {
            Debug.Log("행동력 부족");//it will be additional popupcanvas later
            return;
        }

        SatietyJsonData satieTyJsonData = new SatietyJsonData(DateTime.Now, chargeCount - n, passedTimeInLobby);
        var result = JsonConvert.SerializeObject(satieTyJsonData);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.persistentDataPath, "QuitTimeData"), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(result);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
}
