using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Player : MonoBehaviour, IDamagable
{

    [SerializeField]
    private DOG_INDEX Index;

    [SerializeField]
    public PlayerData playerData;

    [SerializeField]
    public PlayerMoveData playerMoveData = new PlayerMoveData();

    [SerializeField]
    public PlayerJumpData playerJumpData = new PlayerJumpData();

    [SerializeField]
    public PlayerVisualData playerVisualData = new PlayerVisualData();

    [SerializeField] 
    public PlayerSoundData  playerSoundData = new PlayerSoundData();
    
    [SerializeField] public PlayerState playerState;
    [SerializeField] public PlayerHP playerHP;

    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Collider2D playerCollider;
    public Collider2D GetCollider() { return this.playerCollider; }

    [SerializeField] private Rigidbody2D playerRigid;
    private float mOriginGravityScale = 0;
    private float OriginGravityScale {get {return mOriginGravityScale;}}

    public bool stop = false;
    [SerializeField] public BulletShooter bulletShooter;

    public bool IsEnable = false;
    /*********************************************************************************/

    private void Awake()
    {
        /*Set Compoenent*/
        inputHandler ??= GetComponent<InputHandler>();
        playerCollider ??= GetComponent<Collider2D>();
        playerRigid ??= GetComponent<Rigidbody2D>();
        bulletShooter ??= GetComponent<BulletShooter>();
        mOriginGravityScale = playerRigid.gravityScale;

        /*Set PlayerJumpData*/
        playerJumpData.jumpCount = playerJumpData.maxJumpCount;
        if (playerJumpData.groundCheckerTransform == null) { throw new System.Exception("자식 오브젝트에 있는 GroundCheck GameObject 가저올것"); }

        /*Set playerVisualData*/
        playerVisualData.spriteRenderer     ??= GetComponent<SpriteRenderer>();
        playerVisualData.playerAnimator     ??= GetComponent<Animator>();
        //playerVisualData.runningVFXAnimator ??= transform.Find("VFX_RunDust").GetComponent<Animator>();
    }

    private void OnEnable() {
        
    }

    private void Start()
    {
        FlipSprite(1);
        //playerData = GameManager.Instance.CharacterDataTableDesign.GetPlayerDataByINDEX(this.Index); //외부에서 받는것
        /*Set PlayerHP*/
        //playerHP.setHP(playerData.Character_hp);
        //playerHP._recoverHp = playerData.Character_per_hp_heal;
        //bulletShooter.bullets[0].bulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(playerData.Character_bullet_index_1);
        //bulletShooter.bullets[1].bulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(playerData.Character_bullet_index_2);

    }

    public void InitializeAfterAsynchronous(){   
    }
    
    private void Update()
    {
        if(!IsEnable) return;
        inputHandler.TickInput(0);
        Move();
        RestoreJumpCount();
        Jump();
        JumpHold();
        AnimationTick();
        bulletShooter.FireBullet();
        //Debug.DrawRay(transform.position, Vector3.up * 20f, Color.red);
    }

    private void FixedUpdate()
    {
        if(!IsEnable) return;
        if (stop == false) { playerRigid.velocity = new Vector2(playerMoveData.horizontal * playerMoveData.speed, playerRigid.velocity.y); }
        else { playerRigid.velocity = new Vector2(0, playerRigid.velocity.y); }
    }
    /*********************************************************************************/

    #region Visual
    private void FlipSprite(float _horizontal)
    {
        if (_horizontal < 0) { playerVisualData.spriteRenderer.flipX = false; }
        else if (0 < _horizontal) { playerVisualData.spriteRenderer.flipX = true; }
    }
    private void AnimationTick(){
        if(IsCeilHited()){playerVisualData.playerAnimator.SetBool("HitCeil",true);}
        else {playerVisualData.playerAnimator.SetBool("HitCeil",false);}
        playerVisualData.playerAnimator.SetFloat("FallingForce", playerRigid.velocity.y);
        playerVisualData.playerAnimator.SetFloat("MoveSpeed", Mathf.Abs(playerRigid.velocity.x));
        //playerVisualData.runningVFXAnimator.SetFloat("RunSpeed", Mathf.Abs(playerRigid.velocity.x));
        //playerVisualData.runningVFXAnimator.SetBool("IsGround", IsGrounded() && !IsJumped());
    }
    #endregion

    /*********************************************************************************/

    #region Move
    private void Move()
    {
        //horizontal = inputHandler.Horizontal; //키보드 인풋 받기
        playerMoveData.horizontal = 1;
    }

    public void SpeedUp(){
        playerMoveData.speed += 2;
    }
    #endregion

    /*********************************************************************************/

    #region Jump
    public void RestoreJumpCount() { if (IsGrounded() && !IsJumped()) { playerJumpData.jumpCount = playerJumpData.maxJumpCount;} }

    private void Jump()
    {
        if (!inputHandler.CheckJumpInput()) { playerJumpData.IsActivatedOnce = false; } // 점프버튼 뗐는지 체크
        else if (inputHandler.CheckJumpInput())
        {
            if (playerJumpData.isAiring) { return; } //홀드중일떄는 점프 못하게 하기
            if (playerJumpData.jumpCount <= 0) { return; } //점프수 체크
            if (playerJumpData.IsActivatedOnce) { return; } //한번 눌렀는지 체크
            else
            {
                playerRigid.velocity = Vector2.up * playerJumpData.jumpingPower;
                playerJumpData.jumpCount--;
                playerJumpData.IsActivatedOnce = true;
                bulletShooter.FireJumpBullet();
                GameManager.Instance.GlobalSoundManager.PlayByClip(playerSoundData.JumpActive, SOUND_TYPE.SFX);
                if(!IsCeilHited())playerVisualData.playerAnimator.SetTrigger("Jump");
            }
        }
    }

    private void JumpHold(){
        if (!inputHandler.CheckHoldInput() || playerJumpData.isAirHoldPrevented) { 
            playerRigid.gravityScale = OriginGravityScale; 
            playerJumpData.isAiring = false; 
            playerVisualData.playerAnimator.SetBool("IsHolding", false);
        } // 홀드버튼 떼면 다시 하강
        else if (inputHandler.CheckHoldInput())
        {
            if (!playerJumpData.isAirHoldable) { return; } //홀드버튼이 작동 안되면 작동 시키지 말자.
            if (IsGrounded()) { return; } //땅에 있을떄는 작동 못하게 한다.
            playerRigid.gravityScale = 0;
            playerJumpData.isAiring = true;
            playerRigid.velocity = Vector2.right * playerRigid.velocity.x;
            playerVisualData.playerAnimator.SetBool("IsHolding", true);
        }
    }

    private bool IsGrounded()
    {
        Vector2 pointA = Vector2.one * playerJumpData.groundCheckerTransform[0].position;
        Vector2 pointB = Vector2.one * playerJumpData.groundCheckerTransform[1].position;
        Collider2D hit = Physics2D.OverlapArea(pointA, pointB, playerJumpData.groundLayer);
        return hit != null;
    }

    private bool IsCeilHited() {
        Vector2 pointA = Vector2.one * playerJumpData.ceilCheckerTransform[0].position;
        Vector2 pointB = Vector2.one * playerJumpData.ceilCheckerTransform[1].position;
        Collider2D hit = Physics2D.OverlapArea(pointA, pointB, playerJumpData.groundLayer);
        return hit != null;
    }

    private bool IsJumped()
    {
        return !GlobalFunction.GetIsFloatEqual(playerRigid.velocity.y, 0.01f);
    }
    #endregion

    /*********************************************************************************/

    #region GetHit
    public bool IsHitedOnce = false;
    public GameObject HitParticle = null;
    public void GetDamage(float _amount)
    {
        if (IsHitedOnce == true) { return; }
        float currentHp = playerHP.getHP();
        playerHP.setHP(currentHp - _amount);
        var hitObject = ObjectPool.instance.GetObject(HitParticle.gameObject);
        hitObject.transform.position = transform.position;
        hitObject.transform.SetParent(this.transform);
        hitObject.SetActive(true);
        StartCoroutine(AsyncGetDamage());
        GameManager.Instance.GlobalSoundManager.PlayByClip(playerSoundData.GetDamaged, SOUND_TYPE.SFX);
    }

    public bool IsHitable() { return true; }

    IEnumerator AsyncGetDamage()
    {
        IsHitedOnce = true;
        playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
        yield return YieldInstructionCache.WaitForSeconds(1f);
        playerState.ChangeState(PLAYER_STATES.PLAYER_STATE);
        IsHitedOnce = false;
    }

    #endregion

    #region Events
    public void PlayerEnable(){
        this.IsEnable = true;
    }
    public void PlayerDisable(){
        this.IsEnable = false;
    }
    
    Coroutine revivalCoroutine = null;

    public void Heal(float _amount){
        float maxHp = playerHP.getMaxHp();
        float currentHp = playerHP.getHP();
        float healAmount = maxHp * _amount;
        if(healAmount + currentHp >= maxHp) {
            playerHP.setHP(maxHp);
        }
        else {
            playerHP.setHP(currentHp + healAmount);
        }
    }

    public void Barrier(){
        playerState.ChangeState(PLAYER_STATES.BARRIER_STATE);
        Invoke("BecomePlayerState", 10f + 0.2f);
    }
    
    public void Revival(){
        if(revivalCoroutine != null) {StopCoroutine(revivalCoroutine);}
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.RevivalCount--;
        transform.position = GetRevivalPosition();
        revivalCoroutine = StartCoroutine(AsyncRevival());
    }

    IEnumerator AsyncRevival(){
        transform.position = GetRevivalPosition();
        playerRigid.gravityScale = 0;
        playerRigid.velocity = Vector2.right * playerRigid.velocity;
        playerHP.setHP(100f);
        playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
        Invoke("BecomePlayerState", 3f + 0.2f);
        float passedTime = 0;
        while(passedTime <= 3f) {
            passedTime += Time.deltaTime;
            playerRigid.velocity = Vector2.right * playerRigid.velocity;
            if(inputHandler.CheckJumpInput()) {break;}
            yield return null;
        }
        playerRigid.gravityScale = OriginGravityScale;
        playerJumpData.jumpCount = 3;
        yield break;
    }
    public void BecomePlayerState(){
        playerState.ChangeState(PLAYER_STATES.PLAYER_STATE);
    }

    private Vector2 GetRevivalPosition() {
        if(-6f < transform.position.y && transform.position.y < -4f) {
            return new Vector2(transform.position.x, 1f);
        }
        return transform.position;
    }

    #endregion
}