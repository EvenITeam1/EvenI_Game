using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour, Hit
{
    public LayerMask _enemyLayer;
    
    [HideInInspector] public Player player;
    [SerializeField] public BulletData bulletData;
    [SerializeField] public BulletVisualData bulletVisualData;

    public float _lastTime;

    Vector2 _moveDir;
    [SerializeField] Rigidbody2D bulletRigid;

    private void Awake() {
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
        if (other.TryGetComponent(out Mob mob))
        {
            float currentHp = mob.mobHP.getHP();
            float actualDmg = bulletData.Bullet_set_dmg;
            var dmgPrefab = ObjectPool.instance.GetObject(bulletVisualData.dmgPrefab);
            var dmgScript = dmgPrefab.GetComponent<DamageUI>();

            isCrit(ref actualDmg, ref dmgScript);
            
            dmgScript._damage = actualDmg;
            dmgPrefab.transform.position = transform.position;
            
            mob.GetDamage(actualDmg);
            dmgPrefab.SetActive(true);
            ObjectPool.instance.ReturnObject(gameObject);
        }
    }

    public void getDamage(GameObject obj)
    {
    }

    public void setDamage(int dmg)
    {
        bulletData.Bullet_set_dmg = dmg;
    }

    public void setDir()
    {
        float speed = player.PlayerMoveData.speed + bulletData.Bullet_speed;
        bulletRigid.velocity = Vector2.right * speed;
    }

    public void lastLimit()
    {
       bulletData.Bullet_time += Time.deltaTime;

        if (bulletData.Bullet_time >= _lastTime)
        {
            ObjectPool.instance.ReturnObject(gameObject);
        }        
    }

    public void isCrit(ref float actualDmg, ref DamageUI dmgScript)
    {
        bool randomFlag = (Random.Range(0.0f, 1.0f) < bulletData.Bullet_crit_chance) ? true : false;

        if (randomFlag)
        {
            actualDmg *= bulletData.Bullet_crit_const;
            dmgScript._text.color = bulletVisualData.critColor;
            dmgScript._text.fontSize = bulletVisualData.critFontSize;
        }
        else 
        {
            dmgScript._text.color = bulletVisualData.nonCritColor;
            dmgScript._text.fontSize = bulletVisualData.nonCritFontSize;
        }
    }
}
