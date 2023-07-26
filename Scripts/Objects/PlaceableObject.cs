using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceableObject : MonoBehaviour, IDamagable
{
    [SerializeField]
    private OBJECT_INDEX Index;

    [SerializeField]
    public ObjectData objectData;

    [SerializeField]
    public ObjectCoinData coinData = new ObjectCoinData();

    [SerializeField]
    public ObjectItemData itemData = new ObjectItemData();

    [SerializeField]
    public ObjectVisualData visualData = new ObjectVisualData();

    [SerializeField] private BoxCollider2D objectCollider;
    public Collider2D GetCollider() { return this.objectCollider; }

    private List<UnityAction> moveType = new List<UnityAction>();
    private List<UnityAction> itemType = new List<UnityAction>();
    public float moveTriggerOffset = 5f;
    
    private float mObjectHP;
    public float ObjectHP {
        get {return mObjectHP;}
        set {
            mObjectHP = value;
            if(mObjectHP <= 0) {gameObject.SetActive(false);}
        }
    }

    /////////////////////////////////////////////////////////////////////////////////

    #region Basic
    private void Awake()
    {
        /*Set Compoenent*/
        objectCollider ??= GetComponent<BoxCollider2D>();

        /*Set ObjectData*/

        /*Set CoinData*/
        /*Set ItemData*/
    }

    private void OnEnable()
    {
        if(this.objectData.Ob_category == OBJECT_CATEGORY.TRAP){InitMoveTypes();}
        if(this.objectData.Ob_category == OBJECT_CATEGORY.ITEM){InitMoveTypes(); InitItemTypes();}
        
        mObjectHP = objectData.Ob_HP;
        objectCollider.size = new Vector2(objectData.Ob_width, objectData.Ob_height);
    }

    private void Start()
    {
        if(objectData.Ob_movement_index != MOVEMENT_INDEX.HOLD) {
            StartCoroutine(CheckPlayerForMove());
        }
        /*Set ObjectActions*/

        //objectMovementActions[1] = new UnityAction<Collider2D>((_Collider2D) => {HandleControl_001(_Collider2D);});
        //objectData = GameManager.Instance.ObjectDataTableDesign.GetObjectDataByINDEX(this.Index); //외부에서 받는것
    }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (objectData.Ob_category)
        {
            case OBJECT_CATEGORY.DEFAULT: { HandleDefault(other); break; }
            case OBJECT_CATEGORY.TRAP: { HandleTrap(other); break; }
            case OBJECT_CATEGORY.COIN: { HandleCoin(other); break; }
            case OBJECT_CATEGORY.PLATFORM: { HandlePlatform(other); break; }
            case OBJECT_CATEGORY.ITEM: { HandleItem(other); break; }
        }
    }

    public void HandleDefault(Collider2D _other)
    {
        return;
    }

    public void HandleTrap(Collider2D _other)
    {
        if (_other.TryGetComponent(out Player player))
        {
            player.GetDamage(objectData.Ob_damage);
        }
    }
    public void HandleCoin(Collider2D _other)
    {
        if (_other.TryGetComponent(out Player player))
        {
            RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score += this.coinData.ScoreValue;
            Instantiate(this.coinData.particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void HandlePlatform(Collider2D _other)
    {
        return;
    }
    public void HandleItem(Collider2D _other)
    {
        itemType[(int)itemData.ITEM_CATEGORY].Invoke();
        Destroy(gameObject);
        return;
    }
    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region GetHit

    public GameObject HitParticle = null;
    public void GetDamage(float _amount)
    {
        ObjectHP -= _amount;
        Instantiate(HitParticle);
        //StartCoroutine(AsyncOnHitVisual());
    }

    public bool IsHitable() { return objectData.OB_Hitable; }

    IEnumerator AsyncOnHitVisual()
    {
        visualData.spriteRenderer.color = visualData.onHitColor;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        visualData.spriteRenderer.color = visualData.defaultColor;
    }
    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region Movements

    IEnumerator CheckPlayerForMove()
    {
        RaycastHit2D player;
        Vector2 castPos = (Vector2.one * transform.position) + (Vector2.left * moveTriggerOffset) + (Vector2.down * 10);
        Debug.DrawRay(castPos, Vector2.up, Color.cyan, 20f);
        yield return new WaitUntil(
            () => {
                player = Physics2D.Raycast(castPos, Vector2.up, 20f, LayerMask.GetMask("Player", "Ghost"));
                return player;
            }
        );
        moveType[(int)objectData.Ob_movement_index].Invoke();
    }

    public void MovementHold()
    {
        return;
    }

    public void MovementDown()
    {
        transform.DOLocalMove((Vector2.one * transform.position) + (Vector2.down * objectData.Ob_move_strength), 1f)
            .SetEase(Ease.Unset);
    }

    public void MovementUp()
    {
        transform.DOLocalMove((Vector2.one * transform.position) + (Vector2.up * objectData.Ob_move_strength), 1f)
            .SetLoops(1, LoopType.Yoyo);
    }

    public void MovementLeft()
    {
        transform.DOLocalMove((Vector2.one * transform.position) + (Vector2.left * objectData.Ob_move_strength), 1f)
            .SetEase(Ease.Unset);
    }

    public void MovementRight()
    {
        transform.DOLocalMove((Vector2.one * transform.position) + (Vector2.right * objectData.Ob_move_strength), 1f)
            .SetEase(Ease.Unset);
    }

    public void MovementVerticalLoop()
    {
        //visualData.animator.SetTrigger("RunTrigger");
        transform.DOLocalMove((Vector2.one * transform.position) + (Vector2.up * objectData.Ob_move_strength), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);
    }

    public void MovementHorizontalLoop()
    {
        //visualData.animator.SetTrigger("RunTrigger");
        transform.DOLocalMove((Vector2.one * transform.position) + (Vector2.left * objectData.Ob_move_strength), 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutBack);
    }
    public void InitMoveTypes()
    {
        this.moveType.Add(new UnityAction(() => MovementHold()));
        this.moveType.Add(new UnityAction(() => MovementDown()));
        this.moveType.Add(new UnityAction(() => MovementUp()));
        this.moveType.Add(new UnityAction(() => MovementLeft()));
        this.moveType.Add(new UnityAction(() => MovementRight()));
        this.moveType.Add(new UnityAction(() => MovementVerticalLoop()));
        this.moveType.Add(new UnityAction(() => MovementHorizontalLoop()));
    }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    
    #region Items
    public void ItemSmallHeal(){
        RunnerManager.Instance.GlobalPlayer.Heal(0.3f);
        Instantiate(itemData.destroyParticle, transform.position, Quaternion.identity);
        Instantiate(itemData.activateParticle, RunnerManager.Instance.GlobalPlayer.transform);
    }
    public void ItemMediumHeal(){
        RunnerManager.Instance.GlobalPlayer.Heal(0.5f);
        Instantiate(itemData.destroyParticle, transform.position, Quaternion.identity).transform.localScale *= 1.5f;
        Instantiate(itemData.activateParticle, RunnerManager.Instance.GlobalPlayer.transform);

    }

    public void ItemBarrier(){
        RunnerManager.Instance.GlobalPlayer.Barrier();
        Instantiate(itemData.destroyParticle, transform.position, Quaternion.identity);
        Instantiate(itemData.activateParticle, RunnerManager.Instance.GlobalPlayer.transform);
    }

    public void InitItemTypes(){
        this.itemType.Add(new UnityAction(() => {}));
        this.itemType.Add(new UnityAction(() => ItemSmallHeal()));
        this.itemType.Add(new UnityAction(() => ItemMediumHeal()));
        this.itemType.Add(new UnityAction(() => ItemBarrier()));
    }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
}
