using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoDimensions
{
    public class DamageZone : MonoBehaviour
    {
        public GameObject player;
        public int DamageAmount;
        public bool IsKilling;
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Player player)){
                if(IsKilling == true){player.playerHP.die();}
                else {player.GetDamage(DamageAmount);}
            }
        }
    }
}