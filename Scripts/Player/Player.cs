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
    public PlayerMoveData PlayerMoveData = new PlayerMoveData();

    [SerializeField]
    public PlayerJumpData PlayerJumpData = new PlayerJumpData();

    [SerializeField]
    public PlayerVisualData PlayerVisualData = new PlayerVisualData();

    [SerializeField] public PlayerState playerState;
    [SerializeField] public PlayerHP playerHP;

    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Collider2D playerCollider;
    public Collider2D GetCollider() { return this.playerCollider; }

    [SerializeField] private Rigidbody2D playerRigid;
    private float originGravityScale = 0;

    public bool stop = false;
    [SerializeField] public BulletShooter bulletShooter;
    /*********************************************************************************/

    private async void Awake()
    {
        /*Set Compoenent*/
        inputHandler ??= GetComponent<InputHandler>();
        playerCollider ??= GetComponent<Collider2D>();
        playerRigid ??= GetComponent<Rigidbody2D>();
        bulletShooter ??= GetComponent<BulletShooter>();
        originGravityScale = playerRigid.gravityScale;

        /*Set PlayerData*/
        playerData = await GameManager.Instance.CharacterDataTableDesign.GetPlayerDataByINDEX(this.Index); //외부에서 받는것

        /*Set PlayerJumpData*/
        PlayerJumpData.jumpCount = PlayerJumpData.maxJumpCount;
        if (PlayerJumpData.groundCheckerTransform == null) { throw new System.Exception("자식 오브젝트에 있는 GroundCheck GameObject 가저올것"); }

        /*Set PlayerVisualData*/
        PlayerVisualData.spriteRenderer ??= GetComponent<SpriteRenderer>();
        PlayerVisualData.animator ??= GetComponent<Animator>();

        /*Set PlayerHP*/
        playerHP.setHP(playerData.Character_hp);
        playerHP._recoverHp = playerData.Character_per_hp_heal;
    }

    private void Start()
    {
        FlipSprite(1);
    }
    private void Update()
    {
        inputHandler.TickInput(0);
        Move();
        Jump();
        Animations();

        bulletShooter.FireBullet();
    }

    private void FixedUpdate()
    {
       
        if (stop == false) { playerRigid.velocity = new Vector2(PlayerMoveData.horizontal * PlayerMoveData.speed, playerRigid.velocity.y); }
        else { playerRigid.velocity = new Vector2(0, playerRigid.velocity.y); }
    }
    /*********************************************************************************/

    #region Visual
    private void FlipSprite(float _horizontal)
    {
        if (_horizontal < 0) { PlayerVisualData.spriteRenderer.flipX = false; }
        else if (0 < _horizontal) { PlayerVisualData.spriteRenderer.flipX = true; }
    }
    private void Animations()
    {
        PlayerVisualData.animator.SetFloat("MoveSpeed", Mathf.Abs(playerRigid.velocity.x));
    }
    #endregion

    /*********************************************************************************/

    #region Move
    private void Move()
    {
        //horizontal = inputHandler.Horizontal; //키보드 인풋 받기
        PlayerMoveData.horizontal = 1;
    }
    #endregion

    /*********************************************************************************/

    #region Jump
    private void Jump()
    {
        if (IsGrounded() && !IsJumped()) { RestoreJumpCount(); } //점프횟수 복구

        if (!inputHandler.IsJumpPressd) { PlayerJumpData.IsActivatedOnce = false; } // 점프버튼 뗐는지 체크
        else if (inputHandler.IsJumpPressd)
        {
            if (PlayerJumpData.isAiring) { return; } //홀드중일떄는 점프 못하게 하기
            if (PlayerJumpData.jumpCount <= 0) { return; } //점프수 체크
            if (PlayerJumpData.IsActivatedOnce) { return; } //한번 눌렀는지 체크
            else
            {
                playerRigid.velocity = Vector2.up * PlayerJumpData.jumpingPower;
                PlayerJumpData.jumpCount--;
                PlayerJumpData.IsActivatedOnce = true;
                bulletShooter.FireJumpBullet();
            }
        }

        if (!inputHandler.IsAirHoldPressed || PlayerJumpData.isAirHoldPrevented) { 
            playerRigid.gravityScale = originGravityScale; 
            PlayerJumpData.isAiring = false; 
        } // 홀드버튼 떼면 다시 하강
        else if (inputHandler.IsAirHoldPressed)
        {
            if (!PlayerJumpData.isAirHoldable) { return; } //홀드버튼이 작동 안되면 작동 시키지 말자.
            if (IsGrounded()) { return; } //땅에 있을떄는 작동 못하게 한다.
            playerRigid.gravityScale = 0;
            PlayerJumpData.isAiring = true;
            playerRigid.velocity = Vector2.right * playerRigid.velocity.x;
        }

        Debug.DrawRay(PlayerJumpData.groundCheckerTransform.position, Vector3.down * 0.5f, Color.red);
    }
    public void RestoreJumpCount() { PlayerJumpData.jumpCount = PlayerJumpData.maxJumpCount; }

    private bool IsGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(PlayerJumpData.groundCheckerTransform.position, 0.5f, PlayerJumpData.groundLayer);
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
        playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
        return true;
    }
    #endregion
}