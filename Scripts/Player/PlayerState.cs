using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATES
{
    PLAYER_STATE = 0, GHOST_STATE
}

public class PlayerState : MonoBehaviour
{
    public List<Material> MaterialByState = new List<Material>();
    [SerializeField]
    private SpriteRenderer rend;

    private PLAYER_STATES currentState;
    private Coroutine currentCoroutine = null;
    private GameObject playerObject;
    private void Awake()
    {
        playerObject = gameObject;
        rend ??= GetComponent<SpriteRenderer>();
    }

    IEnumerator GHOST_STATE()
    {
        GameManager.Instance.GlobalPlayer.IsHitedOnce = true;
        gameObject.layer = LayerMask.NameToLayer(GlobalStrings.GHOST_STRING);
        rend.material = MaterialByState[(int)PLAYER_STATES.GHOST_STATE];
        yield return YieldInstructionCache.WaitForSeconds(1f);
        GameManager.Instance.GlobalPlayer.IsHitedOnce = false;
        ChangeState(PLAYER_STATES.PLAYER_STATE);
    }

    IEnumerator PLAYER_STATE()
    {
        gameObject.layer = LayerMask.NameToLayer(GlobalStrings.PLAYER_STRING);
        rend.material = MaterialByState[(int)PLAYER_STATES.PLAYER_STATE];
        yield break;
    }

    public void ChangeState(PLAYER_STATES _state)
    {
        Debug.Log($"ChangeState : {_state.ToString()}");
        if (currentState == _state) return;
        currentState = _state;
        currentCoroutine = StartCoroutine(currentState.ToString());
    }
}