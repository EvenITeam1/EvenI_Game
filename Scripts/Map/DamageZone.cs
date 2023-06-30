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
                PlayerHP_Two playerHP = player.GetComponent<PlayerHP_Two>();
                PlayerState playerState = player.GetComponent<PlayerState>();
                if (IsKilling == true) { playerHP.die(); }
                else
                {
                    float currentHp = playerHP.getHP();
                    playerHP.setHP((float)(currentHp - DamageAmount));
                    if(!playerHP.isAlive()){playerHP.die();}
                    playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
                }
            }
        }
    }
}