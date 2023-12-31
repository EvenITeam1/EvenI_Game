using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SatietyJsonData
{
    public DateTime quitTime { get; }
    public int chargeCount { get; }

    public float passedTimeInLobby { get; }

    public SatietyJsonData(DateTime quitTime, int chargeCount, float passedTimeInLobby)
    {
        this.quitTime = quitTime;
        this.chargeCount = chargeCount;
        this.passedTimeInLobby = passedTimeInLobby;
    }
}
