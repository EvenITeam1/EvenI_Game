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
    // public float _lastTime;

    protected override void Awake()
    {
        player = GameManager.Instance.GlobalPlayer;
        bulletRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastLimit();
    }

    void OnEnable()
    {
        bulletData.Bullet_time = 0;
        setDamage((int)Random.Range(bulletData.Bullet_min_dmg, bulletData.Bullet_max_dmg + 1f));
        setDir();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            float currentHp = player.playerHP.getHP();
            float actualDmg = bulletData.Bullet_set_dmg;

            isCrit(ref actualDmg);

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
    //public void lastLimit()
    //public void isCrit(ref float actualDmg, ref DamageUI dmgScript)
    public void isCrit(ref float actualDmg)
    {
        bool randomFlag = (Random.Range(0.0f, 1.0f) < bulletData.Bullet_crit_chance) ? true : false;

        if (randomFlag) {
            actualDmg *= bulletData.Bullet_crit_const;
        }
    }
}
