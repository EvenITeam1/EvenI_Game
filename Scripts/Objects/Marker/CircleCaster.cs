using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public class CircleCaster : EventMarker
{
    //public LayerMask castTargets;
    //protected RaycastHit2D[] hits;
    public LayerMask castTargets;
    [SerializeField] UnityEvent OnTriggerEvents;
    
    protected override void FixedUpdate() {
        hits = GetCastedTarget();
        if(hits.Count() == 0) {isActivated = false;}
        else if(hits.Count() != 0 && !isActivated)
        {
            isActivated = true;
            OnTriggerEvents.Invoke();
            Destroy(gameObject);
        }
    }
    protected override RaycastHit2D[] GetCastedTarget()
    {
        return hits = Physics2D.CircleCastAll(transform.position, 0.75f, Vector2.zero, castTargets);
    }
}
