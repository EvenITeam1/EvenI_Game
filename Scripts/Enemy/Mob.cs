using DG.Tweening;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Mob : MonoBehaviour, IDamagable
{
    [SerializeField] 
    private MOB_INDEX Index;
    
    [SerializeField]
    public MobData mobData ;
    
    [HideInInspector]
    public MobMoveData mobMoveData;

    [SerializeField] public MobHP mobHP;

    [SerializeField] private Collider2D mobCollider;
    public Collider2D GetCollider() { return this.mobCollider; }
    [SerializeField] private Rigidbody2D mobRigid;
    [SerializeField] public BulletShooter bulletShooter;

    [HideInInspector]
    public bool IsInstantiatedFirst = false;

    Coroutine mobLifeCycleCoroutine;
    private void Awake(){
        /*Set Compoenent*/
        mobCollider ??= GetComponent<Collider2D>();
        mobCollider.enabled = false;
        mobRigid = GetComponent<Rigidbody2D>();

        /*Set MobData*/
        //mobData = ;
        
        /*Set MobHP*/
        
        gameObject.SetActive(false);
    }

    private void Update() {
        bulletShooter.FireBullet();
    }

    private void OnEnable() {
        if(!IsInstantiatedFirst) {return;}
        InitMoveTypes();
        mobHP.setHP(mobData.Mob_hp);
        if(!mobMoveData.IsInfiniteLifetime){mobLifeCycleCoroutine = StartCoroutine(InstantiateMobByAlert());}
        else {InstantiateMob();}
    }

    #region Insantiate
    IEnumerator InstantiateMobByAlert(){
        RunnerManager.Instance.GlobalMobGenerator.GenerateAlertObject(
            new Vector2(RunnerManager.Instance.GlobalMobGenerator.arrowposition, transform.localPosition.y) + (Vector2.left * 1.5f)
        );
        yield return YieldInstructionCache.WaitForSeconds(1f);
        InvokeMovement();
        yield return YieldInstructionCache.WaitForSeconds(mobMoveData.invokeEaseTime);
        mobCollider.enabled = true;
        mobMoveData.moveType[(int)mobData.Mob_movement_index].Invoke();
        yield return YieldInstructionCache.WaitForSeconds(mobMoveData.lifeTime);
        RunnerManager.Instance.GlobalMobGenerator.GenerateAlertObject(
            new Vector2(transform.localPosition.x, transform.localPosition.y) + (Vector2.left * 1.5f)
        );
        yield return YieldInstructionCache.WaitForSeconds(1f);
        ExitMovement();
    }

    public void InstantiateMob(){
        InvokeMovement();
    }

    #endregion
    
    #region Movement
    public void InvokeMovement(){
        transform.DOLocalMove(mobMoveData.invokePosition, mobMoveData.invokeEaseTime)
            .SetEase(mobMoveData.invokeEase);
    }

    public void MovementHold(){
        return;
    }
    
    public void MovementDown(){
        transform.DOLocalMove(mobMoveData.invokePosition + Vector2.down * mobMoveData.movementStrength, 1f)
            .SetEase(Ease.Unset);
    }
    
    public void MovementUp(){
        transform.DOLocalMove(mobMoveData.invokePosition + Vector2.up * mobMoveData.movementStrength, 1f)
            .SetEase(Ease.Unset);
    }
    
    public void MovementLeft(){
        transform.DOLocalMove(mobMoveData.invokePosition + Vector2.left * mobMoveData.movementStrength, 1f)
            .SetEase(Ease.Unset);
    }
    
    public void MovementRight(){
        transform.DOLocalMove(mobMoveData.invokePosition + Vector2.right * mobMoveData.movementStrength, 1f)
            .SetEase(Ease.Unset);
    }
    
    public void MovementVerticalLoop(){
        transform.DOLocalMove(mobMoveData.invokePosition + Vector2.up * mobMoveData.movementStrength, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);
    }
    
    public void MovementHorizontalLoop(){
        transform.DOLocalMove(mobMoveData.invokePosition + Vector2.left * mobMoveData.movementStrength, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);
    }
    public void ExitMovement(){
        if(mobMoveData.IsInfiniteLifetime){return;}
        transform.DOLocalMove(mobMoveData.exitPosition, mobMoveData.exitEaseTime)
            .SetEase(mobMoveData.exitEase);
    }

    public void InitMoveTypes(){
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementHold()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementDown()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementUp()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementLeft()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementRight()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementVerticalLoop()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementHorizontalLoop()));
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

    private void OnDisable() {
        if(IsInstantiatedFirst) {
            transform.DOKill();
            mobCollider.enabled = false;
            if(mobLifeCycleCoroutine != null) StopCoroutine(mobLifeCycleCoroutine);
        }
    }

    #endregion
}