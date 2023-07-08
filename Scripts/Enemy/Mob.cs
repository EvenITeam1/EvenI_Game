using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Mob : MonoBehaviour, IDamagable
{
    [SerializeField] 
    private MOB_INDEX Index;
    
    [SerializeField]
    public MobData mobData;
    
    [SerializeField]
    public MobMoveData mobMoveData;

    [SerializeField] public MobHP mobHP;

    [SerializeField] private Collider2D mobCollider;
    public Collider2D GetCollider() { return this.mobCollider; }
    [SerializeField] private Rigidbody2D mobRigid;

    private void Awake(){
        /*Set Compoenent*/
        mobCollider ??= GetComponent<Collider2D>();
        mobRigid = GetComponent<Rigidbody2D>();

        /*Set MobData*/
        //mobData = ;
        
        /*Set MobHP*/
        mobHP.setHP(mobData.Mob_hp);
    }

    private async void OnEnable() {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        InvokeMovement();
        await UniTask.Delay(TimeSpan.FromSeconds(mobMoveData.GetLifeTime()));
        ExitMovement();
    }
    
    #region Movement
    public void InvokeMovement(){
        transform.DOLocalMoveX(mobMoveData.invokePosition, mobMoveData.invokeEaseTime).SetEase(mobMoveData.invokeEase);
    }

    public void ExitMovement(){
        if(mobMoveData.IsInfiniteLifetime){return;}
        transform.DOLocalMoveX(mobMoveData.exitPosition, mobMoveData.exitEaseTime).SetEase(mobMoveData.exitEase);
    }
    #endregion
    
    #region GetHit
    public GameObject HitParticle = null;
    public bool GetDamage(float _amount) {
        float currentHp = mobHP.getHP();
        mobHP.setHP(currentHp - _amount);
        Instantiate(HitParticle, transform);
        return true;
    }

    #endregion
}