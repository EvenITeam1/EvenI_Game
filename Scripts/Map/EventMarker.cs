using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventMarker : MonoBehaviour
{
    public UnityEvent Event;
    RaycastHit2D hit;
    private void FixedUpdate() {
        hit = Physics2D.Raycast(transform.position, Vector2.down, float.NegativeInfinity);
    }
}