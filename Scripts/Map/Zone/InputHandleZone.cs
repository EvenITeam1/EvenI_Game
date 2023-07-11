using UnityEngine;

public class InputHandleZone : Zone
{
    public Player player;

    private void Start()
    {
        player = GameManager.Instance.GlobalPlayer;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
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
    public override void TriggerAction(Collider2D _other) { }
}