using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MobGenMarker : EventMarker 
{
    // public bool isActivated;
    // protected RaycastHit2D[] hits;
    public List<MobGenData> mobElements;

    protected override void Awake() {
        if(mobElements == null){throw new System.Exception("리스트가 비어있습니다.");}
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
            RunnerManager.Instance.GlobalMobGenerator.GenerateMobs(mobElements);
            Destroy(gameObject);
        }
    }

}