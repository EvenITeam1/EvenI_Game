using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EventMarker : MonoBehaviour
{
    [SerializeField] private string inputString;
    [SerializeField]  UnityEvent<string> PrintString;
    
    [SerializeField] UnityEvent OnTriggerEvents;

    public bool isActivated;
    protected RaycastHit2D[] hits;
    protected virtual void FixedUpdate()
    {
        hits = GetCastedTarget();
        var player = (
            from hit in hits  
            where (hit.collider.name == GlobalStrings.LAYERS_STRING[(int)PROJECT_LAYERS.Player])  
            select hit
        );

        if(player.Count() == 0) {isActivated = false;}
        else if(player.Count() != 0 && !isActivated)
        {
            isActivated = true;
            PrintString.Invoke(inputString);
            OnTriggerEvents.Invoke();
            Destroy(gameObject);
        }
    }
    protected virtual RaycastHit2D[] GetCastedTarget()
    {
        return hits = Physics2D.RaycastAll(transform.position, Vector2.down, float.NegativeInfinity);
    }
}