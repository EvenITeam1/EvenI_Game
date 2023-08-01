using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class OutsideSatietyManage : MonoBehaviour
{
    [SerializeField] List<Image> guage;
    [SerializeField] Color fillColor;
    public int chargeCount;
    public int chargeTimeBySec;
    public DateTime prevTime = DateTime.MinValue;
    private void Start()
    {
        chargeCount = 10;
    }
    private void Update()
    {

    }


    public void recordLobbyOutTime()//use in Button
    {
        prevTime = DateTime.Now;
    }
    public void CalculateCount()
    {
        if (isFull())
            return;

        else
        {
            TimeSpan passedTime = DateTime.Now - prevTime;
            int plusCount = (int)passedTime.TotalSeconds / chargeTimeBySec;
            int remainTime = (int)passedTime.TotalSeconds % chargeTimeBySec;
            chargeCount += plusCount;
            if (chargeCount > 10)
                chargeCount = 10;



        }      
    }

    public void refreshGuageColor()
    {
        for (int i = 0; i < chargeCount; i++)        
            guage[i].color = fillColor;       
    }

    public bool isFull()
    {
        if (chargeCount == 10)
            return true;

        else
            return false;
    }

}
