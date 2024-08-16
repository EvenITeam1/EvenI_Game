using System;

[Serializable]
public class FreeItemJsonData
{
    public DateTime quitTime { get; }
    public double passedTimeInLobby1 { get; }
    public double passedTimeInLobby2 { get; }
    public double passedTimeInLobby3 { get; }
    public bool adReady1 { get; }
    public bool adReady2 { get; }
    public bool adReady3 { get; }

    public FreeItemJsonData(DateTime quitTime, double passedTimeInLobby1, double passedTimeInLobby2, double passedTimeInLobby3, bool adReady1, bool adReady2, bool adReady3)
    {
        this.quitTime = quitTime;
        this.passedTimeInLobby1 = passedTimeInLobby1;
        this.passedTimeInLobby2 = passedTimeInLobby2;
        this.passedTimeInLobby3 = passedTimeInLobby3;
        this.adReady1 = adReady1;
        this.adReady2 = adReady2;
        this.adReady3 = adReady3;
    }
}
