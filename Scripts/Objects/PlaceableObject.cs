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
        objectData = GameManager.Instance.ObjectDataTableDesign.objectDataforms[(int)Index - ObjectData.indexBasis];

        /*Set CoinData*/
        /*Set ItemData*/
    }

    private void Start() {
        objectCollider.size = new Vector2(objectData.Ob_width, objectData.Ob_height);
        objectCollider.offset = new Vector2(0, objectData.Ob_floor);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter");
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
           Debug.Log(player.GetDamage(objectData.Ob_damage));
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
}