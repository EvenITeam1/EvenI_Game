using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int  DamageAmount;
    public bool IsKilling;
    void OnTriggerEnter2D(Collider2D other) {
        PlayerHP playerHP = other.GetComponent<PlayerHP>();
        if(IsKilling == true){playerHP.die();}
        else {
            float currentHp = playerHP.getHP();
            playerHP.setHP((float)(currentHp - DamageAmount));
        }
    }
}