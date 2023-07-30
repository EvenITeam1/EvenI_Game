using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSO
{
    public class AnimEventListener : MonoBehaviour
    {
        public RaycastPlayer Character;

        public void StopAttacks()
        {
            Character.StopAttacks();
        }

        public void SpawnAttack(int attackIndex)
        {
            Character.SpawnAttackEffect(attackIndex);
        }

        public void StopAttackEffects()
        {
            Character.StopAttackEffects();
        }

        public void FinishedDying()
        {
            Character.FinishedDying();
        }

        public void Dazed()
        {
            Character.Dazed();
        }

        public void FinishedStun()
        {
            Character.FinishedStun();
        }
    }
}