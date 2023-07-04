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
            if (player.GetComponent<Collider2D>() == other)
            {
                PlayerHP playerHP = player.GetComponent<PlayerHP>();
                PlayerState playerState = player.GetComponent<PlayerState>();
                if (IsKilling == true) { playerHP.die(); }
                else
                {
                    float currentHp = playerHP.getHP();
                    playerHP.setHP((float)(currentHp - DamageAmount));
                    playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
                }
            }
        }
    }
}