using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, Fire
{
    [SerializeField] GameObject _enemyBullet;
    float _time = 0;
    [SerializeField] float _coolTime;

    void Update()
    {
        if(_time < _coolTime)    
            _time += Time.deltaTime;
        else
        {
            fireBullet();
            _time = 0;
        } 
    }

    public void fireBullet()
    {
        GameObject bullet = ObjectPool.instance.GetObject(_enemyBullet);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }
}
