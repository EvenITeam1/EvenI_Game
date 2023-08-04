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
    public MobData mobData;

    [HideInInspector]
    public MobMoveData mobMoveData;
    public MobVisualData visualData;
    public MobSoundData soundData;

    [SerializeField] public MobHP mobHP;

    [SerializeField] private Collider2D mobCollider;
    public Collider2D GetCollider() { return this.mobCollider; }
    [SerializeField] private Rigidbody2D mobRigid;
    [SerializeField] public BulletShooter bulletShooter;
    public bool bulletEnable = false;

    [HideInInspector]
    public bool IsInstantiatedFirst = false;

    private Vector2 VisiblePos;
    private Vector2 DestroyPos;

    Coroutine mobLifeCycleCoroutine;
    /////////////////////////////////////////////////////////////////////////////////

    #region Basic
    private void Awake()
    {
        /*Set Compoenent*/
        mobCollider ??= GetComponent<Collider2D>();
        mobCollider.enabled = false;
        
        mobRigid = GetComponent<Rigidbody2D>();

        /*Set MobData*/
        if(GlobalFunction.GetIsFloatEqual(mobMoveData.invokePosition.x + 12, 0.001f)){
            throw new Exception("mob의 발생 위치가 -12라면 이제 앞으로 0을 기준으로 작성해 주시면 됩니다.");
        }
        if(GlobalFunction.GetIsFloatEqual(mobMoveData.exitPosition.x + 50, 0.001f)){
            throw new Exception("mob의 제거 위치가 -50라면 이제 앞으로 작성을 0으로해도 괜찮습니다 Y만 작성해주시면 되겠습니다.");
        }


        /*Set MobHP*/
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(bulletEnable) {bulletShooter.FireBullet();}
    }

    private void OnEnable()
    {
        if (!IsInstantiatedFirst) { return; }
        VisiblePos = mobMoveData.invokePosition + Vector2.one * RunnerManager.Instance.GlobalMobGenerator.VisiableTransform.localPosition + Vector2.left * 25f;
        DestroyPos = mobMoveData.exitPosition +  Vector2.one * RunnerManager.Instance.GlobalMobGenerator.DestroyTransform.localPosition + Vector2.left * 25f;
        InitMoveTypes();
        mobHP.setHP(mobData.Mob_hp);
        mobLifeCycleCoroutine = StartCoroutine(InstantiateMobByAlert());
    }

    private void Start()
    {
        //bulletShooter.bullets[0].bulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(mobData.Mob_bullet_index);
    }
    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region Insantiate
    IEnumerator InstantiateMobByAlert()
    {
        
        RunnerManager.Instance.GlobalMobGenerator.GenerateAlertObject(VisiblePos + Vector2.left * 1.5f);
        yield return YieldInstructionCache.WaitForSeconds(1f);
        InvokeMovement();
        yield return YieldInstructionCache.WaitForSeconds(mobMoveData.invokeEaseTime);
        mobCollider.enabled = true;
        bulletEnable = true;
        mobMoveData.moveType[(int)mobData.Mob_movement_index].Invoke();
        if(mobMoveData.IsInfiniteLifetime){yield break;}
        yield return YieldInstructionCache.WaitForSeconds(mobMoveData.lifeTime);
        transform.DOKill();
        RunnerManager.Instance.GlobalMobGenerator.GenerateAlertObject(Vector2.one * transform.localPosition + Vector2.left * 1.5f);
        yield return YieldInstructionCache.WaitForSeconds(1f);
        ExitMovement();
    }
    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region Trigger
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.TryGetComponent(out Player player))
        {
            player.GetDamage(10f);
        }
    }
    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region Movement
    public void InvokeMovement()
    {
        transform.DOLocalMove(VisiblePos, mobMoveData.invokeEaseTime)
            .SetEase(mobMoveData.invokeEase);
    }

    public void MovementHold()
    {
        return;
    }

    public void MovementDown()
    {

        transform.DOLocalMove(VisiblePos + Vector2.down * mobData.Mob_move_strength, 1f)
            .SetEase(Ease.Unset);
    }

    public void MovementUp()
    {
        transform.DOLocalMove(VisiblePos + Vector2.up * mobData.Mob_move_strength, 1f)
            .SetEase(Ease.Unset);
    }

    public void MovementLeft()
    {
        transform.DOLocalMove(VisiblePos + Vector2.left * mobData.Mob_move_strength, 1f)
            .SetEase(Ease.Unset);
    }

    public void MovementRight()
    {
        transform.DOLocalMove(VisiblePos + Vector2.right * mobData.Mob_move_strength, 1f)
            .SetEase(Ease.Unset);
    }

    public void MovementVerticalLoop()
    {        
        transform.DOLocalMove(VisiblePos + Vector2.up * mobData.Mob_move_strength, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);
    }

    public void MovementHorizontalLoop()
    {
        transform.DOLocalMove(VisiblePos + Vector2.left * mobData.Mob_move_strength, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);
    }
    public void ExitMovement()
    {
        if (mobMoveData.IsInfiniteLifetime) { return; }
        transform.DOLocalMove(DestroyPos, mobMoveData.exitEaseTime)
            .SetEase(mobMoveData.exitEase);
    }

    public void InitMoveTypes()
    {
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementHold()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementDown()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementUp()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementLeft()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementRight()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementVerticalLoop()));
        this.mobMoveData.moveType.Add(new UnityAction(() => MovementHorizontalLoop()));
    }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region GetHit

    public GameObject HitParticle = null;
    public void GetDamage(float _amount)
    {
        float currentHp = mobHP.getHP();
        mobHP.setHP(currentHp - _amount);
        var hitObject = ObjectPool.instance.GetObject(HitParticle.gameObject);
        hitObject.transform.position = transform.position;
        hitObject.transform.SetParent(this.transform);
        hitObject.transform.localScale = hitObject.transform.parent.localScale * Vector2.one;
        hitObject.SetActive(true);
        StartCoroutine(AsyncOnHitVisual());
        GameManager.Instance.GlobalSoundManager.PlayByClip(soundData.GetDamaged, SOUND_TYPE.SFX);
    }

    public bool IsHitable() { return true; }

    IEnumerator AsyncOnHitVisual()
    {
        visualData.spriteRenderer.color = visualData.onHitColor;
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        visualData.spriteRenderer.color = visualData.defaultColor;
    }
    

    private void OnDisable()
    {
        if (IsInstantiatedFirst)
        {
            transform.DOKill();
            mobCollider.enabled = false;
            if (mobLifeCycleCoroutine != null) StopCoroutine(mobLifeCycleCoroutine);
        }
    }

    private void OnDestroy() {
        if(mobData.Mob_Score > 0)
            RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score += mobData.Mob_Score;        
    }

    #endregion
}