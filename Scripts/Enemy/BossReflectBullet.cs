using System.Collections;
using System.Collections.Generic;
using TwoDimensions;
using UnityEngine;

public class BossReflectBullet : Bullet
{
    //public LayerMask _enemyLayer;
    //
    //[HideInInspector] public Player player;
    //[SerializeField] public BulletData bulletData;
    //[SerializeField] public BulletVisualData bulletVisualData;
    // [SerializeField]  protected Rigidbody2D bulletRigid;
    Vector2 lastDir;
    int reflectCount;

    protected override void Awake()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastLimit();
        lastDir = bulletRigid.velocity;
    }

    void OnEnable()
    {
        player = RunnerManager.Instance.GlobalPlayer;
        time = 0;
        reflectCount = 0;
        setInitialDir();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>())
            collision.gameObject.GetComponent<Player>().GetDamage(bulletData.Bullet_set_dmg);

        else if (collision.gameObject.GetComponent<ReflectAxisX>() || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            ReflectAxisX();

        else if (collision.gameObject.GetComponent<ReflectAxisY>())
            ReflectAxisY();

        if (reflectCount == bulletData.Bullet_reflect_count)
            ObjectPool.instance.ReturnObject(gameObject);
    }

    public void setInitialDir()
    {
        bulletRigid.velocity = transform.right * bulletData.Bullet_speed;
    }

    public void ReflectAxisX()
    {
        Vector2 reflectedDir = new Vector2(lastDir.x, -lastDir.y).normalized;
        bulletRigid.velocity = reflectedDir * bulletData.Bullet_speed;
        reflectCount++;
    }

    public void ReflectAxisY()
    {
        Vector2 reflectedDir = new Vector2(-lastDir.x, lastDir.y).normalized;
        bulletRigid.velocity = reflectedDir * bulletData.Bullet_speed;
        reflectCount++;
    }
}
