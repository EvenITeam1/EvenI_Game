using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventMarker : MonoBehaviour
{
    [SerializeField] private string inputString;
    [SerializeField] private UnityEvent<string> PrintString;
    public bool isActivated;
    protected RaycastHit2D hit;
    protected virtual void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, float.NegativeInfinity);
        if (hit.collider != null)
        {
            if (hit.collider.name == "Player" && !isActivated)
            {
                isActivated = true;
                PrintString.Invoke(inputString);
            }
            if (hit.collider.name != "Player")
            {
                isActivated = false;
            }
        }
    }
    protected virtual RaycastHit2D GetCastedTarget()
    {
        return hit = Physics2D.Raycast(transform.position, Vector2.down, float.NegativeInfinity);
    }
}