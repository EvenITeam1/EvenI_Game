using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InsideSatietyManage : MonoBehaviour
{
    [SerializeField] OutsideSatietyManage outsideSatietyManage;
    public float initialRemainTime;
    public int chargeTimeBySec;
    float remainTime;
    float time;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(!outsideSatietyManage.isFull())
            time = Time.time;
    }

    private void Update()
    {
        timeCheck();
    }

    void getRemainTimeFromOutside()
    {
    }

    void timeCheck()
    {
        if (outsideSatietyManage.isFull())
        {
            initialRemainTime = outsideSatietyManage.chargeTimeBySec;
            return;
        }

        else
        {
            remainTime = initialRemainTime - time;
            if (remainTime < 0)
            {
                outsideSatietyManage.chargeCount++;
            }
        }
    }
}
