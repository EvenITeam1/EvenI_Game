using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationAchivementChecker : MonoBehaviour
{
    [SerializeField] Transform StartTransform;
    [SerializeField] Transform EndTransform;
    [SerializeField] Transform Player;

    public float MapDistance;

    private void Awake() {
        MapDistance = Mathf.Round(EndTransform.position.x - StartTransform.position.x);
    }

    public float GetRunningAmount(){
        return StartTransform.position.x - Player.position.x;
    }
    public float GetRemainAmount(){
        return EndTransform.position.x - GetRemainAmount();
    }

    public float GetRatioOfDistance(){
        return 100f-((MapDistance - Player.position.x) / MapDistance) * 100f;
    }
}
