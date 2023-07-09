using System.Collections;
using System.Collections.Generic;
using TwoDimensions;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, Hit
{
    public LayerMask _playerLayer;
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
            getDamage(collision.gameObject);
            ObjectPool.instance.ReturnObject(gameObject);
        }
    }

    public void getDamage(GameObject obj)
    {
        PlayerHP playerScript = obj.GetComponent<PlayerHP>();
        PlayerState playerState = obj.GetComponent<PlayerState>();
        float currentHp = playerScript.getHP();
        playerScript.setHP(currentHp - _damage);
        playerState.ChangeState(PLAYER_STATES.GHOST_STATE);
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
