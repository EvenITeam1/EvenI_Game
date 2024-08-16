using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class FreeItemManage : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> leftTimeTextsList;
    [SerializeField] List<GameObject> AdPagesList;
    [SerializeField] List<GameObject> TimePagesList;
    public List<float> initialChargeTime;
    float chargeTimeBySec1;
    float chargeTimeBySec2;
    float chargeTimeBySec3;
    float remainTime1 = 1;
    float remainTime2 = 1;
    float remainTime3 = 1;
    float timeLeft1;
    float timeLeft2;
    float timeLeft3;
    double passedTimeInLobby1;
    double passedTimeInLobby2;
    double passedTimeInLobby3;
    public static DateTime prevTime = DateTime.MaxValue;
    bool isFirst1 = false;
    bool isFirst2 = false;
    bool isFirst3 = false;

    //Triggers
    public static bool adReady1;
    public static bool adReady2;
    public static bool adReady3;

    private void Awake()
    {
       LoadFreeItemDataFromJson();
    }
    private void Start()
    {
        RevivalCalculateOutside();
        SatietyCalculateOutside();
        CoinCalculateOutside();  
    }
    private void Update()
    {
        RefreshState();
        RevivalCalculateInside();
        SatietyCalculateInside();
        CoinCalculateInside();
    }

    public void RevivalCalculateInside()
    {
        if (adReady1) { }

        else if (remainTime1 > 0)
        {
            if(isFirst1)
            {
                chargeTimeBySec1 = initialChargeTime[0] + Time.unscaledTime;
                isFirst1 = false;
            }
            remainTime1 = chargeTimeBySec1 - Time.unscaledTime;
            passedTimeInLobby1 = initialChargeTime[0] - remainTime1;
            int leftHour = (int)remainTime1 / 3600;
            int leftMin = (int)(remainTime1 % 3600 / 60);
            int leftSec = (int)(remainTime1 % 3600 % 60);
            leftTimeTextsList[0].text = $"리필까지 {leftHour}시간 {leftMin}분 {leftSec}초";
        }

        else
        {
            adReady1 = true;
            remainTime1 = 1;
            isFirst1 = true;
        }
    }

    public void SatietyCalculateInside()
    {
        if (adReady2) { }

        else if (remainTime2 > 0)
        {
            if (isFirst2)
            {
                chargeTimeBySec2 = initialChargeTime[1] + Time.unscaledTime;
                isFirst2 = false;
            }

            remainTime2 = chargeTimeBySec2 - Time.unscaledTime;
            passedTimeInLobby2 = initialChargeTime[1] - remainTime2;
            int leftHour = (int)remainTime2 / 3600;
            int leftMin = (int)(remainTime2 % 3600 / 60);
            int leftSec = (int)(remainTime2 % 3600 % 60);
            leftTimeTextsList[1].text = $"리필까지 {leftHour}시간 {leftMin}분 {leftSec}초";
        }

        else
        {
            adReady2 = true;
            remainTime2 = 1;
            isFirst2 = true;
        }
    }

    public void CoinCalculateInside()
    {
        if (adReady3) { }

        else if (remainTime3 > 0)
        {
            if (isFirst3)
            {
                chargeTimeBySec3 = initialChargeTime[2] + Time.unscaledTime;
                isFirst3 = false;
            }

            remainTime3 = chargeTimeBySec3 - Time.unscaledTime;
            passedTimeInLobby3 = initialChargeTime[2] - remainTime3;
            int leftHour = (int)remainTime3 / 3600;
            int leftMin = (int) (remainTime3 % 3600 / 60);
            int leftSec = (int)(remainTime3 % 3600 % 60);
            leftTimeTextsList[2].text = $"리필까지 {leftHour}시간 {leftMin}분 {leftSec}초";
        }

        else
        {
            adReady3 = true;
            remainTime3 = 1;
            isFirst3 = true;
        }
    }

    public void RevivalCalculateOutside()
    {
        if (adReady1)
        {
            timeLeft1 = 0;
            isFirst1 = true;
        }

        else
        {
            TimeSpan dt = DateTime.Now - prevTime;
            if (dt.TotalHours < 0) { return; }
            int n = (int)(dt.TotalSeconds + passedTimeInLobby1) / (int)initialChargeTime[0];

            if (n > 0)
            {
                adReady1 = true;
                timeLeft1 = 0;
                isFirst1 = true;
            }

            else
            {              
                timeLeft1 = (float)(dt.TotalSeconds + passedTimeInLobby1);
                adReady1 = false;
                chargeTimeBySec1 = initialChargeTime[0] - timeLeft1 + Time.unscaledTime;
                isFirst1 = false;
            }
        }
    }

    public void SatietyCalculateOutside()
    {
        if (adReady2) 
        {
            timeLeft2 = 0;
            isFirst2 = true;
        }

        else
        {
            TimeSpan dt = DateTime.Now - prevTime;
            if (dt.TotalHours < 0) { return; }
            int n = (int)(dt.TotalSeconds + passedTimeInLobby2) / (int)initialChargeTime[1];

            if (n > 0)
            {
                adReady2 = true;
                timeLeft2 = 0;
                isFirst2 = true;
            }

            else
            {
                timeLeft2 = (int)(dt.TotalSeconds + passedTimeInLobby2);
                adReady2 = false;
                chargeTimeBySec2 = initialChargeTime[1] - timeLeft2 + Time.unscaledTime;
                isFirst2 = false;
            }
              
        }
    }
    public void CoinCalculateOutside()
    {
        if (adReady3) 
        {
            timeLeft3 = 0;
            isFirst3 = true;
        }

        else
        {
            TimeSpan dt = DateTime.Now - prevTime;
            if (dt.TotalHours < 0) { return; }
            int n = (int)(dt.TotalSeconds + passedTimeInLobby3) / (int)initialChargeTime[2];
            Debug.Log("n : " + n);
            if (n > 0)
            {
                adReady3 = true;
                timeLeft3 = 0;
                isFirst3 = true;
            }

            else
            {
                timeLeft3 = (int)(dt.TotalSeconds + passedTimeInLobby3);
                adReady3 = false;
                chargeTimeBySec3 = initialChargeTime[2] - timeLeft3 + Time.unscaledTime;
                isFirst3 = false;
            }              
        }
    }

    public void SaveFreeItemDataToJson()
    {
        FreeItemJsonData JsonData = new FreeItemJsonData(DateTime.Now, passedTimeInLobby1, passedTimeInLobby2, passedTimeInLobby3, adReady1, adReady2, adReady3);
        var result = JsonConvert.SerializeObject(JsonData);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.persistentDataPath, "FreeItemData"), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(result);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void LoadFreeItemDataFromJson()
    {
        if (!File.Exists(string.Format("{0}/{1}.json", Application.persistentDataPath, "FreeItemData")))
        {
            Debug.Log("최초-광고아이템");
            adReady1 = true;
            adReady2 = true;
            adReady3 = true;
        }

        else
        {
            string JsonFileText = File.ReadAllText(string.Format("{0}/{1}.json", Application.persistentDataPath, "FreeItemData"));
            FreeItemJsonData JsonData = JsonConvert.DeserializeObject<FreeItemJsonData>(JsonFileText);
            Debug.Log(JsonFileText);
            prevTime = JsonData.quitTime;
            passedTimeInLobby1 = JsonData.passedTimeInLobby1;
            passedTimeInLobby2 = JsonData.passedTimeInLobby2;
            passedTimeInLobby3 = JsonData.passedTimeInLobby3;
            adReady1 = JsonData.adReady1;
            adReady2 = JsonData.adReady2;
            adReady3 = JsonData.adReady3;
        }
    }

    void RefreshState()
    {
        if(adReady1)
        {
            AdPagesList[0].SetActive(true);
            TimePagesList[0].SetActive(false);
        }

        else
        {
            AdPagesList[0].SetActive(false);
            TimePagesList[0].SetActive(true);
        }

        if (adReady2)
        {
            AdPagesList[1].SetActive(true);
            TimePagesList[1].SetActive(false);
        }

        else
        {
            AdPagesList[1].SetActive(false);
            TimePagesList[1].SetActive(true);
        }

        if (adReady3)
        {
            AdPagesList[2].SetActive(true);
            TimePagesList[2].SetActive(false);
        }

        else
        {
            AdPagesList[2].SetActive(false);
            TimePagesList[2].SetActive(true);
        }
    }
}
