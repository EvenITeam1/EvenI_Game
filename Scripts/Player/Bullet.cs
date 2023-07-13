using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour, Hit
{
    public LayerMask _enemyLayer;
    
    [SerializeField]
    private BULLET_INDEX Index;
    public Player player;
    [SerializeField] public BulletData bulletData;
    [SerializeField] public BulletVisualData bulletVisualData;

    public float _lastTime;
    [SerializeField] 
    protected Rigidbody2D bulletRigid;

    protected virtual void Awake() {
        bulletRigid = GetComponent<Rigidbody2D>();
        player = RunnerManager.Instance.GlobalPlayer;
    }
    private void Start() {
        //bulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(this.Index);
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
            dmgPrefab.SetActive(true);
            
            mob.GetDamage(actualDmg);
            ObjectPool.instance.ReturnObject(gameObject);
        }
        else if(other.TryGetComponent(out BossHP boss))
        {
            float currentHp = boss.getHP();
            float actualDmg = bulletData.Bullet_set_dmg;
            
            var dmgPrefab = ObjectPool.instance.GetObject(bulletVisualData.dmgPrefab);
            var dmgScript = dmgPrefab.GetComponent<DamageUI>();

            isCrit(ref actualDmg, ref dmgScript);

            dmgScript._damage = actualDmg;
            dmgPrefab.transform.position = transform.position;
            dmgPrefab.SetActive(true);

            boss.setHP(currentHp - actualDmg);
            boss.updateHpBar();
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

    public virtual void setDir()
    {
        float speed = player.playerMoveData.speed + bulletData.Bullet_speed;
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