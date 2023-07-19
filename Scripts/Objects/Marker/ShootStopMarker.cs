using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ShootStopMarker : EventMarker
{
    // [SerializeField] private string inputString;
    // [SerializeField] private UnityEvent<string> PrintString;
    // public bool isActivated;
    // protected RaycastHit2D[] hits;
    protected override void FixedUpdate()
    {
        hits = GetCastedTarget();
        var player = (
            from hit in hits  
            where (hit.collider.tag == GlobalStrings.LAYERS_STRING[(int)PROJECT_LAYERS.Player])  
            select hit
        );

        if(player.Count() == 0) {isActivated = false;}
        else if(player.Count() != 0 && !isActivated)
        {
            //Debug.Log("지나침");
            isActivated = true;
            foreach (var item in player)
            {
                item.collider.GetComponent<Player>().bulletShooter.IsFireable = false;
            }
        }
    }
}