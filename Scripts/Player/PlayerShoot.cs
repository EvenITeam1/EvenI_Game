using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject _playerBullet;
    [SerializeField] GameObject _jumpBullet;
    float _time = 0;
    [SerializeField] float _coolTime;

    void Update()
    {
        if (_time < _coolTime)
            _time += Time.deltaTime;
        else
        {
            fireBullet();
            _time = 0;
        }
    }

    public void fireBullet()
    {
        GameObject bullet = ObjectPool.instance.GetObject(_playerBullet);
        bullet.transform.position = transform.position;
        var bulletScript = bullet.GetComponent<PlayerBullet>();
        bulletScript._playerRigid = GetComponent<Rigidbody2D>();
        bullet.SetActive(true);
    }

    public void fireJumpBullet()
    {
        GameObject bullet = ObjectPool.instance.GetObject(_jumpBullet);
        bullet.transform.position = transform.position;
        var bulletScript = bullet.GetComponent<PlayerBullet>();
        bulletScript._playerRigid = GetComponent<Rigidbody2D>();
        bullet.SetActive(true);
    }
}
