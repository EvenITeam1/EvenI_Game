using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceableObject : MonoBehaviour {
    [SerializeField]
    private OBJECT_INDEX    Index;

    [SerializeField] 
    public ObjectData objectData;

    [SerializeField] 
    public ObjectCoinData coinData = new ObjectCoinData();

    [SerializeField] 
    public ObjectItemData itemData = new ObjectItemData();

    [SerializeField] private BoxCollider2D         objectCollider;
        public Collider2D GetCollider() {return this.objectCollider;}
    
    private UnityAction<Collider2D>[] objectMovementActions;

    private void Awake() {
        /*Set Compoenent*/
        objectCollider  ??= GetComponent<BoxCollider2D>();

        /*Set ObjectData*/
        objectData = GameManager.Instance.ObjectDataTableDesign.GetObjectDataByINDEX(this.Index); //외부에서 받는것
        /*Set CoinData*/
        /*Set ItemData*/
    }

    private void Start() {
        /*Set ObjectActions*/
        objectMovementActions = new UnityAction<Collider2D>[10];
        objectCollider.size = new Vector2(objectData.Ob_width, objectData.Ob_height);
        
        objectMovementActions[0] = new UnityAction<Collider2D>((_Collider2D) => {StaticMovement_000(_Collider2D);});
        //objectMovementActions[1] = new UnityAction<Collider2D>((_Collider2D) => {HandleControl_001(_Collider2D);});

        ExtDebug.DrawBoxCastBox(transform.position, Vector2.one/2, Quaternion.identity, Vector2.zero, 1, Color.blue);
    }

    /////////////////////////////////////////////////////////////////////////////////
#region Trigger
    private void OnTriggerEnter2D(Collider2D other) {
        switch (objectData.Ob_category)
        {
            case OBJECT_CATEGORY.DEFAULT : {HandleDefault(other); break;}
            case OBJECT_CATEGORY.TRAP : {HandleTrap(other); break;}
            case OBJECT_CATEGORY.COIN : {HandleCoin(other); break;}
            case OBJECT_CATEGORY.PLATFORM : {HandlePlatform(other); break;}
            case OBJECT_CATEGORY.ITEM : {HandleItem(other); break;}
        }
    }

    public void HandleDefault(Collider2D _other){
        return;
    }
    public void HandleTrap(Collider2D _other){
        if(_other.TryGetComponent(out Player player)){
           player.GetDamage(objectData.Ob_damage);
           objectMovementActions[(int)objectData.Ob_movement].Invoke(_other);
        }
    }
    public void HandleCoin(Collider2D _other){
        if(_other.TryGetComponent(out Player player)){
            GameManager.Instance.GlobalEventInstance.scoreCheck.Score +=this.coinData.ScoreValue;
            Instantiate(this.coinData.particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void HandlePlatform(Collider2D _other){
        return;
    }
    public void HandleItem(Collider2D _other){
        return;
    }
#endregion
    /////////////////////////////////////////////////////////////////////////////////

#region Movements
    public void StaticMovement_000(Collider2D _other){
        Debug.Log("아무것도 안함"); 
        return; 
    }
    // public void HandleControl_001(Collider2D _other){ 
    //     if(_other.TryGetComponent(out Player player)){
    //         Debug.Log("플레이어 검증");
    //     }
    // }
    // IEnumerator IHandleControl_001(Player _player) {
    //     _player.PlayerJumpData.isAirHoldable = false;
    //     yield return new WaitWhile( () => {
    //             RaycastHit2D playerLayerHIt = Physics2D.BoxCast(
    //                 transform.position, 
    //                 Vector2.one, 
    //                 0, Vector2.zero, 0, 
    //                 LayerMask.NameToLayer(GlobalStrings.PLAYER_STRING)
    //             );
    //             if(playerLayerHIt == false) {Debug.Log("빠져나왔다");}
    //             return playerLayerHIt;
    //         }
    //     );
    //     _player.PlayerJumpData.isAirHoldable = true;
    // }
#endregion
}