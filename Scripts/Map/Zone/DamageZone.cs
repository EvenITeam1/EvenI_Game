using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : Zone
{
    public int DamageAmount;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        TriggerAction(other);
    }

    public override void TriggerAction(Collider2D _other){
        if (_other.TryGetComponent(out Player player))
        {
            player.playerHP.die();
        }
    }
}