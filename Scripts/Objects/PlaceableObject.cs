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
            

    private void Awake() {
        /*Set Compoenent*/
        objectCollider  ??= GetComponent<BoxCollider2D>();

        /*Set ObjectData*/
        objectData = GameManager.Instance.ObjectDataTableDesign.objectDataforms[(int)this.Index];

        /*Set CoinData*/
        /*Set ItemData*/
    }

    private void Start() {
        objectCollider.size = new Vector2(objectData.Ob_width, objectData.Ob_height);
        objectCollider.offset = new Vector2(0, objectData.Ob_floor);
    }

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
            PlayerHP playerHP = player.playerHP;
            PlayerState playerState = player.playerState;
            
            float currentHp = player.playerHP.getHP();
            playerHP.setHP((float)(currentHp - objectData.Ob_damage));
            playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
        }
    }
    public void HandleCoin(Collider2D _other){
        if(_other.TryGetComponent(out ScoreCheck scoreCheck)){
            scoreCheck.Score += this.coinData.ScoreValue;
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
}