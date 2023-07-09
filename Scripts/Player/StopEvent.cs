using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopEvent : MonoBehaviour
{
    public string inputString;
    public UnityEvent<string> PrintString;

    RaycastHit2D hit;
    [SerializeField] GameObject upMirror;

    private void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, float.NegativeInfinity);
        if (hit.collider != null && hit.collider.name == "Player")
        {
            PrintString.Invoke(inputString);
            upMirror.SetActive(true);
            hit.collider.GetComponent<Player>().stop = true;
            hit.collider.GetComponent<PlayerShoot>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
