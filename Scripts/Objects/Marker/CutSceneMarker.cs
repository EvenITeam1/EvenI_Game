using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CutSceneMarker : EventMarker
{
    // [SerializeField] private string inputString;
    // [SerializeField] private UnityEvent<string> PrintString;
    // public bool isActivated;
    // protected RaycastHit2D[] hits;
    public GameObject CutSceneUI;
    protected override void FixedUpdate()
    {
        hits = GetCastedTarget();
        var player = (
            from hit in hits  
            where (hit.collider.name == GlobalStrings.PLAYER_STRING)  
            select hit
        );

        if(player.Count() == 0) {isActivated = false;}
        else if(player.Count() != 0 && !isActivated)
        {
            isActivated = true;
            //SomeEvent
            Instantiate(CutSceneUI, Vector3.zero, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}