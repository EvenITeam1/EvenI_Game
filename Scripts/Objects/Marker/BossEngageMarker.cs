using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BossEngageMarker : EventMarker
{
    // [SerializeField] private string inputString;
    // [SerializeField] private UnityEvent<string> PrintString;
    // public bool isActivated;
    // protected RaycastHit2D[] hits;

    public GameObject bossHPUI;
    public GameObject bossObj;
    
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
            isActivated = true;
            foreach (var item in player)
            {
                item.collider.GetComponent<Player>().stop = true;
                item.collider.GetComponent<Player>().bulletShooter.IsFireable = true;
            }
            gameObject.SetActive(false);
            bossObj.SetActive(true);
            bossHPUI.SetActive(true);
        }
    }
}
