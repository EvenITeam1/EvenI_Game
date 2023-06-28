using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;
=======
using TMPro;
using UnityEngine;
>>>>>>> develop2

public class PlayerBullet : MonoBehaviour, Hit
{
    public LayerMask _enemyLayer;
<<<<<<< HEAD
    [SerializeField] Rigidbody2D _bulletRigid;
    public int _damage;
    public int _setDmg;
    float _time;
    public float _lastTime;
    Vector2 _moveDir;
    public float _bulletSpeed;
=======
    public int _minDmg;
    public int _maxDmg;
    int _damage;
    public float _critChance;
    public float _critConst;//크리티컬 배율
    float _time;
    public float _lastTime;
    Vector2 _moveDir;
    [SerializeField] Rigidbody2D _bulletRigid;
    public float _bulletSpeed;
    [SerializeField] GameObject _dmgPrefab;
    [SerializeField] int _critFontSize;
    [SerializeField] int _nonCritFontSize;
    [SerializeField] Color32 _critColor;
    [SerializeField] Color32 _nonCritColor;
>>>>>>> develop2

    void Update()
    {
        lastLimit();
    }

    void OnEnable()
    {
        _time = 0;
<<<<<<< HEAD
        setDamage(_setDmg);
=======
        setDamage(Random.Range(_minDmg, _maxDmg + 1));
>>>>>>> develop2
        setDir();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHP>())
        {
            getDamage(collision.gameObject);
            ObjectPool.instance.ReturnObject(gameObject);
        }
    }
<<<<<<< HEAD
    public void getDamage(GameObject obj)
    {
        EnemyHP enemyScript = obj.GetComponent<EnemyHP>();
        float currentHp = enemyScript.getHP();
        enemyScript.setHP(currentHp - _damage);
=======
    public void getDamage(GameObject obj)//obj에 데미지를 가하는 함수
    {
        EnemyHP enemyScript = obj.GetComponent<EnemyHP>();
        float currentHp = enemyScript.getHP();
        float actualDmg = _damage;
        var dmgPrefab = ObjectPool.instance.GetObject(_dmgPrefab);
        var dmgScript = dmgPrefab.GetComponent<DamageUI>();

        if (isCrit())//크리티컬이 터지면 실질적으로 들어갈 데미지에 크리티컬 배율을 곱해준다.
        {
            actualDmg *= _critConst;
            dmgScript._text.color = _critColor;
            dmgScript._text.fontSize = _critFontSize;
        }

        else 
        {
            dmgScript._text.color = _nonCritColor;
            dmgScript._text.fontSize = _nonCritFontSize;
        }

       
        dmgScript._damage = actualDmg;
        dmgPrefab.transform.position = transform.position;
        dmgPrefab.SetActive(true);

        enemyScript.setHP(currentHp - actualDmg);
>>>>>>> develop2
        enemyScript.updateHpBar();
        if (!enemyScript.isAlive())
        {
            enemyScript.die();
        }
    }

<<<<<<< HEAD
    public void setDamage(int dmg)
=======
    public void setDamage(int dmg)//투사체의 데미지를 정하는 함수
>>>>>>> develop2
    {
        _damage = dmg;
    }

<<<<<<< HEAD
    public void setDir()
=======
    public void setDir()//투사체가 나갈 방향을 정하는 함수
>>>>>>> develop2
    {
        _moveDir = new Vector2(1, 0);
        _bulletRigid.velocity = _moveDir * _bulletSpeed;
    }

<<<<<<< HEAD
    public void lastLimit()
=======
    public void lastLimit() //정해진 지속시간(_lastTime)이 경과하면 투사체를 씬에서 제거하는 함수
>>>>>>> develop2
    {
       _time += Time.deltaTime;

        if (_time >= _lastTime)
        {
            ObjectPool.instance.ReturnObject(gameObject);
        }        
    }
<<<<<<< HEAD
=======
    public bool isCrit()//정해진 크리티컬 확률(_critChance)에 따라 크리티컬 인지 아닌지를 반환하는 함수
    {
        if (Random.Range(0.0f, 1.0f) < _critChance)
        {
            return true;
        }

        else
            return false;
    }

>>>>>>> develop2
}
