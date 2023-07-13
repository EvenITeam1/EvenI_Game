using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] public Bullet[] bullets;
    float _time = 0;
    [SerializeField] float _coolTime;

    public bool IsFireable;

    public void FireBullet()
    {
        if(!IsFireable) {return;}
        if (_time < _coolTime) {_time += Time.deltaTime; return;}
        else {
            GameObject bullet = ObjectPool.instance.GetObject(bullets[0].gameObject);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            _time = 0;
        }
    }

    public void FireJumpBullet()
    {
        if(!IsFireable) {return;}
        GameObject bullet = ObjectPool.instance.GetObject(bullets[1].gameObject);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }
}
