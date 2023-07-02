using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoDimensions{
    
    public enum PLAYER_STATES {
        DEFAULT_STATE = 0, GHOST_STATE
    }

    public class PlayerState : MonoBehaviour {
        public List<Color>          ColorsByState = new List<Color>();
        [SerializeField] 
        private SpriteRenderer      rend;
        
        private PLAYER_STATES       currentState;
        private Coroutine           currentCoroutine = null;
        private GameObject          playerObject;
        private void Awake() {
            playerObject = gameObject;
            rend ??= GetComponent<SpriteRenderer>();
        }

        IEnumerator GHOST_STATE(){
            gameObject.layer = LayerMask.NameToLayer(GlobalStrings.GHOST_STRING);
            yield return YieldInstructionCache.WaitForSeconds(1f);
            ChangeState(PLAYER_STATES.DEFAULT_STATE);
        }

        IEnumerator DEFAULT_STATE()
        {
            gameObject.layer = LayerMask.NameToLayer(GlobalStrings.DEFAULT_STRING);
            yield break;
        }
        
        public void ChangeState(PLAYER_STATES _state){
            if(currentState == _state) return;
            currentState = _state;
            rend.material.color = ColorsByState[(int)_state];
            currentCoroutine = StartCoroutine(currentState.ToString());
        }
    }
}
