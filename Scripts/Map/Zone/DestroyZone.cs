using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : Zone
{
    protected override void OnTriggerEnter2D(Collider2D other) 
    {
        TriggerAction(other);
    }

    public override void TriggerAction(Collider2D _other)
    {
        if(_other.TryGetComponent(out Mob mob)){
            Destroy(mob.gameObject);
        }
    }
}