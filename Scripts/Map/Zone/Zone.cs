using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zone : MonoBehaviour{
    protected abstract void OnTriggerEnter2D(Collider2D other);
    public abstract void TriggerAction(Collider2D _other);
}