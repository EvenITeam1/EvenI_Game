using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Mob mob)){
            Destroy(mob.gameObject);
        }
    }

}