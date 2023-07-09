using System.Collections;
using System.Collections.Generic;
using TwoDimensions;
using UnityEngine;

public class EnemyReflectBullet : MonoBehaviour
{
    public LayerMask _playerLayer;
    int _damage;
    public int _setDmg;
    float _time;
    public float _lastTime;
    public float _bulletSpeed;
    public int _maxReflectCount;
    public Rigidbody2D _bulletRigid;
    Vector2 _lastDir;
    int _reflectCount;
    void Update()
    {
        lastLimit();
        _lastDir = _bulletRigid.velocity;
    }

    void OnEnable()
    {
        _time = 0;
        _reflectCount = 0;
        setDamage(_setDmg);
        setInitialDir();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>())
            collision.gameObject.GetComponent<Player>().GetDamage(_damage);

        else if (collision.gameObject.GetComponent<ReflectAxisX>() || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            ReflectAxisX();

        else if (collision.gameObject.GetComponent<ReflectAxisY>())
            ReflectAxisY();

        if (_reflectCount == _maxReflectCount)
            ObjectPool.instance.ReturnObject(gameObject);
    }

    public void setDamage(int dmg)
    {
        this._damage = dmg;
    }
    public void lastLimit()
    {
        _time += Time.deltaTime;

        if (_time >= _lastTime)
        {
            ObjectPool.instance.ReturnObject(gameObject);
        }
    }

    public void setInitialDir()
    {
        _bulletRigid.velocity = transform.right * _bulletSpeed;
    }

    public void ReflectAxisX()
    {
        Vector2 reflectedDir = new Vector2(_lastDir.x, -_lastDir.y).normalized;
        _bulletRigid.velocity = reflectedDir * _bulletSpeed;
        _reflectCount++;
    }

    public void ReflectAxisY()
    {
        Vector2 reflectedDir = new Vector2(-_lastDir.x, _lastDir.y).normalized;
        _bulletRigid.velocity = reflectedDir * _bulletSpeed;
        _reflectCount++;
    }
}
