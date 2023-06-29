using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TwoDimensions
{
    public class EventMarker : MonoBehaviour
    {
        public string inputString;
        public UnityEvent<string> PrintString;

        bool isActivated;
        RaycastHit2D hit;
        private void FixedUpdate()
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, float.NegativeInfinity);
            if (hit.collider.name == "Player" && !isActivated){
                isActivated = true;
                PrintString.Invoke(inputString);
            }
            if(hit.collider.name != "Player") {
                isActivated = false;
            }
        }
    }
}
