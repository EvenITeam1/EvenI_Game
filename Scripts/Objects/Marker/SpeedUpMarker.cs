using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SpeedUpMarker : EventMarker {
    // public bool isActivated;
    // protected RaycastHit2D[] hits;
    protected override void Awake(){

    }
    protected override void FixedUpdate() {
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
            player.Any((E) => {
                if(E.transform.TryGetComponent(out Player playerComponent)) {
                    playerComponent.SpeedUp();
                    return true;
                }
                return false;
            });
        }
    }
}