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
        playerVisualData.spriteRenderer ??= GetComponent<SpriteRenderer>();
        playerVisualData.animator ??= GetComponent<Animator>();

    }

    private void OnEnable() {
        playerData = GameManager.Instance.CharacterDataTableDesign.GetPlayerDataByINDEX(this.Index); //외부에서 받는것
        /*Set PlayerHP*/
        playerHP.setHP(playerData.Character_hp);
        playerHP._recoverHp = playerData.Character_per_hp_heal;
        //bulletShooter.bullets[0].bulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(playerData.Character_bullet_index_1);
        //bulletShooter.bullets[1].bulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(playerData.Character_bullet_index_2);
    }

    private void Start()
    {
        FlipSprite(1);
    }

    public void InitializeAfterAsynchronous(){   
    }
    
    private void Update()
    {
        if(!IsEnable) return;
        inputHandler.TickInput(0);
        Move();
        Jump();
        Animations();
        bulletShooter.FireBullet();
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
    private void Animations()
    {
        playerVisualData.animator.SetFloat("MoveSpeed", Mathf.Abs(playerRigid.velocity.x));
    }
    #endregion

    /*********************************************************************************/

    #region Move
    private void Move()
    {
        //horizontal = inputHandler.Horizontal; //키보드 인풋 받기
        playerMoveData.horizontal = 1;
    }
    #endregion

    /*********************************************************************************/

    #region Jump
    private void Jump()
    {
        if (IsGrounded() && !IsJumped()) { RestoreJumpCount(); } //점프횟수 복구

        if (!inputHandler.IsJumpPressd) { playerJumpData.IsActivatedOnce = false; } // 점프버튼 뗐는지 체크
        else if (inputHandler.IsJumpPressd)
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
            }
        }

        if (!inputHandler.IsAirHoldPressed || playerJumpData.isAirHoldPrevented) { 
            playerRigid.gravityScale = OriginGravityScale; 
            playerJumpData.isAiring = false; 
        } // 홀드버튼 떼면 다시 하강
        else if (inputHandler.IsAirHoldPressed)
        {
            if (!playerJumpData.isAirHoldable) { return; } //홀드버튼이 작동 안되면 작동 시키지 말자.
            if (IsGrounded()) { return; } //땅에 있을떄는 작동 못하게 한다.
            playerRigid.gravityScale = 0;
            playerJumpData.isAiring = true;
            playerRigid.velocity = Vector2.right * playerRigid.velocity.x;
        }

        Debug.DrawRay(playerJumpData.groundCheckerTransform.position, Vector3.down * 0.5f, Color.red);
    }
    public void RestoreJumpCount() { playerJumpData.jumpCount = playerJumpData.maxJumpCount; }

    private bool IsGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(playerJumpData.groundCheckerTransform.position, 0.5f, playerJumpData.groundLayer);
        return hit != null;
    }

    private bool IsJumped()
    {
        return !GlobalFunction.GetIsFloatEqual(playerRigid.velocity.y, 0.001f);
    }
    #endregion

    /*********************************************************************************/

    #region GetHit
    public bool IsHitedOnce = false;
    public GameObject HitParticle = null;
    public bool GetDamage(float _amount){
        if(IsHitedOnce == true) {return false;}
        float currentHp = playerHP.getHP();
        playerHP.setHP(currentHp - _amount);
        Instantiate(HitParticle, transform);
        StartCoroutine(AsyncGetDamage());
        return true;
    }

    IEnumerator AsyncGetDamage(){
        RunnerManager.Instance.GlobalPlayer.IsHitedOnce = true;
        playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
        yield return YieldInstructionCache.WaitForSeconds(1f);
        playerState.ChangeState(PLAYER_STATES.PLAYER_STATE);
        RunnerManager.Instance.GlobalPlayer.IsHitedOnce = false;
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
    public void Revival(){
        if(revivalCoroutine != null) {StopCoroutine(revivalCoroutine);}
        GameManager.Instance.GlobalSaveNLoad.saveData.outGameData.RevivalCount--;
        
        revivalCoroutine = StartCoroutine(AsyncRevival());
    }
    IEnumerator AsyncRevival(){
        transform.position = new Vector2(transform.position.x, 3f);
        playerRigid.gravityScale = 0;
        playerRigid.velocity = Vector2.right * playerRigid.velocity;
        playerHP.setHP(100f);
        playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
        float passedTime = 0;
        while(passedTime <= 3f) {
            passedTime += Time.deltaTime;
            playerRigid.velocity = Vector2.right * playerRigid.velocity;
            if(inputHandler.IsJumpPressd) {break;}
            yield return null;
        }
        playerRigid.gravityScale = OriginGravityScale;
        playerJumpData.jumpCount = 3;
        playerState.ChangeState(PLAYER_STATES.PLAYER_STATE);
        yield break;
    }
    #endregion
}