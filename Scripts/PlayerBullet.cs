using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBullet : MonoBehaviour, Hit
{
    public LayerMask _enemyLayer;
    public int _damage;
    public int _setDmg;
    float _time;
    public float _lastTime;
    Vector2 _moveDir;
    [SerializeField] Rigidbody2D _bulletRigid;
    public float _bulletSpeed;

    void Update()
    {
        lastLimit();
    }

    void OnEnable()
    {
        _time = 0;
        setDamage(_setDmg);
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
    public void getDamage(GameObject obj)
    {
        EnemyHP enemyScript = obj.GetComponent<EnemyHP>();
        float currentHp = enemyScript.getHP();
        enemyScript.setHP(currentHp - _damage);
        enemyScript.updateHpBar();
        if (!enemyScript.isAlive())
        {
            enemyScript.die();
        }
    }

    public void setDamage(int dmg)
    {
        _damage = dmg;
    }

    public void setDir()
    {
        _moveDir = new Vector2(1, 0);
        _bulletRigid.velocity = _moveDir * _bulletSpeed;
    }

    public void lastLimit()
    {
       _time += Time.deltaTime;

        if (_time >= _lastTime)
        {
            ObjectPool.instance.ReturnObject(gameObject);
        }        
    }
}
