using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, Hit
{
    public LayerMask _playerLayer;
    public int _damage;
    public int _setDmg;
    float _time;
    public float _lastTime;
    public float _bulletSpeed;
    [SerializeField] Rigidbody2D _bulletRigid;
    public Vector2 _moveDir;
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
        if (collision.gameObject.GetComponent<PlayerHP>())
        {
            getDamage(collision.gameObject);
            ObjectPool.instance.ReturnObject(gameObject);
        }
    }

    public void getDamage(GameObject obj)
    {
        PlayerHP playerScript = obj.GetComponent<PlayerHP>();
        float currentHp = playerScript.getHP();
        playerScript.setHP(currentHp - _damage);
        if (!playerScript.isAlive())
        {
            playerScript.die();
        }
    }

    public void setDamage(int dmg)
    {
        this._damage = dmg;
    }

    public void setDir()
    {
        _moveDir = -transform.right;
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
