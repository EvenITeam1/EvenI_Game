using System.Collections;
using System.Collections.Generic;
using TwoDimensions;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int _damage;
    public int _setDmg;
    float _time;
    public float _lastTime;
    public float _bulletSpeed;
    public Rigidbody2D _bulletRigid;
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
            collision.gameObject.GetComponent<Player>().GetDamage(_damage);
            ObjectPool.instance.ReturnObject(gameObject);
        }
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

    public void setDir() 
    {
        _bulletRigid.velocity = transform.right * _bulletSpeed;

    }
}
