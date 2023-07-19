using System.Collections;
using System.Collections.Generic;
using TwoDimensions;
using UnityEngine;

public class BossBasicBullet : Bullet
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
        bulletRigid = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        player = RunnerManager.Instance.GlobalPlayer;
        bulletData.Bullet_time = 0;
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
            float actualDmg = bulletData.Bullet_set_dmg;
            player.GetDamage(actualDmg);
            ObjectPool.instance.ReturnObject(gameObject);
        }
    }

    //public void setDamage(int dmg)
    // public void getDamage(GameObject obj)

    public override void setDir()
    {
        bulletRigid.velocity = transform.right * bulletData.Bullet_speed;
    }
    //public void lastLimit()
    //public void isCrit(ref float actualDmg, ref DamageUI dmgScript)
}
