using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlayer : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 positionOffsets;
    private void Awake() {
        positionOffsets = transform.position;
    }
    void FixedUpdate()
    {
        transform.position = (Vector3.right * target.position.x) + positionOffsets;
    }
}
