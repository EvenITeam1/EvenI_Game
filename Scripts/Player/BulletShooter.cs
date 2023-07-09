using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] Bullet playerBullet;
    [SerializeField] Bullet jumpBullet;
    float _time = 0;
    [SerializeField] float _coolTime;

    public void FireBullet()
    {
        if (_time < _coolTime) {_time += Time.deltaTime; return;}
        else {
            GameObject bullet = ObjectPool.instance.GetObject(playerBullet.gameObject);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            _time = 0;
        }
    }

    public void FireJumpBullet()
    {
        GameObject bullet = ObjectPool.instance.GetObject(jumpBullet.gameObject);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }
}
