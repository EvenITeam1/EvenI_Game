using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EventMarker : MonoBehaviour
{
    [SerializeField] private string inputString;
    [SerializeField] private UnityEvent<string> PrintString;
    public bool isActivated;
    protected RaycastHit2D[] hits;
    protected virtual void FixedUpdate()
    {
        hits = GetCastedTarget();
        var player = (
            from hit in hits  
            where (hit.collider.name == GlobalStrings.PLAYER_STRING)  
            select hit
        );

        if(player == null) {isActivated = false;}
        else if(player != null && !isActivated)
        {
            isActivated = true;
            PrintString.Invoke(inputString);
        }
    }
    protected virtual RaycastHit2D[] GetCastedTarget()
    {
        return hits = Physics2D.RaycastAll(transform.position, Vector2.down, float.NegativeInfinity);
    }
}