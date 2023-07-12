using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlayer : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 positionOffsets;
    public bool IsEnable = true;
    private void Awake() {
        positionOffsets = transform.position;
    }
    void FixedUpdate()
    {
        if(IsEnable) transform.position = (Vector3.right * target.position.x) + positionOffsets;
    }
    public void FlowEnable(){this.IsEnable = true;}
    public void FlowDisable(){this.IsEnable = false;}
}
