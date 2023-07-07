using UnityEngine;

namespace TwoDimensions
{
    public class InputHandleZone : MonoBehaviour
    {
        public Player player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Checked");
            if (player.GetCollider() == other)
            {
                player.PlayerJumpData.isAirHoldable = false;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (player.GetCollider() == other)
            {
                player.PlayerJumpData.isAirHoldable = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (player.GetCollider() == other)
            {
                player.PlayerJumpData.isAirHoldable = true;
            }
        }
    }
}