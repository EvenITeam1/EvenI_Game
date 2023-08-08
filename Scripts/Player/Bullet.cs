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
    [SerializeField] public BulletSoundData bulletSoundData;

    [HideInInspector] public float time;
    [SerializeField]
    protected Rigidbody2D bulletRigid;

    protected virtual void Awake()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        lastLimit();
    }

    void OnEnable()
    {
        player = RunnerManager.Instance.GlobalPlayer;
        time = 0;
        setDamage((int)Random.Range(bulletData.Bullet_min_dmg, bulletData.Bullet_max_dmg + 1f));
        setDir();
        GameManager.Instance.GlobalSoundManager.PlayByClip(bulletSoundData.Shoot, SOUND_TYPE.SFX);
    }
    private void Start()
    {
        //bulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(this.Index);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") return;
        if (other.TryGetComponent(out IDamagable damagable))
        {
            //Debug.Log("IDamagable");
            if (!damagable.IsHitable()) { return; }
            float actualDmg = bulletData.Bullet_set_dmg;
            var dmgPrefab = ObjectPool.instance.GetObject(bulletVisualData.dmgPrefab);
            var dmgScript = dmgPrefab.GetComponent<DamageUI>();

            isCrit(ref actualDmg, ref dmgScript);

            dmgScript._damage = actualDmg;
            dmgPrefab.transform.position = transform.position;
            dmgPrefab.SetActive(true);
            damagable.GetDamage(actualDmg);

            ObjectPool.instance.ReturnObject(gameObject);
        }
        else if (other.TryGetComponent(out BossHP boss))
        {
            float currentHp = boss.getHP();
            float actualDmg = bulletData.Bullet_set_dmg;

            var dmgPrefab = ObjectPool.instance.GetObject(bulletVisualData.dmgPrefab);
            var dmgScript = dmgPrefab.GetComponent<DamageUI>();

            isCrit(ref actualDmg, ref dmgScript);

            dmgScript._damage = actualDmg;
            dmgPrefab.transform.position = transform.position;
            dmgPrefab.SetActive(true);
            boss.GetDamaged();

            if (boss.isBossRaid)
                RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score += actualDmg;
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
        time += Time.deltaTime;

        if (time >= bulletData.Bullet_last_time)
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

