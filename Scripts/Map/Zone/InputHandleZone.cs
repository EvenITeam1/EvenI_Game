using UnityEngine;

namespace TwoDimensions
{
    public class InputHandleZone : MonoBehaviour
    {
        public Player player;

        private void Start() {
            player = GameManager.Instance.GlobalPlayer;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (player.GetCollider() == other)
            {
                player.PlayerJumpData.isAirHoldPrevented = true;
                player.PlayerJumpData.isAirHoldable = false;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (player.GetCollider() == other)
            {
                player.PlayerJumpData.isAirHoldPrevented = true;
                player.PlayerJumpData.isAirHoldable = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (player.GetCollider() == other)
            {
                player.PlayerJumpData.isAirHoldPrevented = false;
                player.PlayerJumpData.isAirHoldable = true;
            }
        }
    }
}