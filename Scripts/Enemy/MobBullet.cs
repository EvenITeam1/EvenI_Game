using System.Collections;
using System.Collections.Generic;
using TwoDimensions;
using UnityEngine;

public class MobBullet : Bullet
{
    //public LayerMask _enemyLayer;
    //
    //[HideInInspector] public Player player;
    //[SerializeField] public BulletData bulletData;
    //[SerializeField] public BulletVisualData bulletVisualData;
    // [SerializeField]  protected Rigidbody2D bulletRigid;

    protected override void Awake()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        player = RunnerManager.Instance.GlobalPlayer;
        time = 0;
        setDir();
    }

    void Update()
    {
        lastLimit();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            float currentHp = player.playerHP.getHP();
            float actualDmg = bulletData.Bullet_set_dmg;

            player.GetDamage(actualDmg);
            ObjectPool.instance.ReturnObject(gameObject);
        }
    }

    //public void setDamage(int dmg)
    // public void getDamage(GameObject obj)
    
    public override void setDir()
    {
        float speed = (player.playerMoveData.speed - bulletData.Bullet_speed) + 5f;
        bulletRigid.velocity = Vector2.left * speed;
    }
}
