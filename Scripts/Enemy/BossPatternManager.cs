using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    float _time;
    [SerializeField] float _coolTime;

    [SerializeField] GameObject[] laserObj;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject basicBullet;
    [SerializeField] GameObject reflectBullet;

    public Vector2 _exampleVector;
    
    void Update()
    {
        pattern2();
    }

    void pattern1()//example
    {
        if (_time < _coolTime)
        {
            _time += Time.deltaTime;
        }

        else
        {
            int n = Random.Range(0, 2);
            SetEnemyLaser.executeLaser(laserObj[n]);
            _time = 0;
        }
    }

    void pattern2()//example
    {
        if(_time < _coolTime)
        {
            _time += Time.deltaTime;
        }

        else
        {
            SetEnemyBullet.fireBullet(player.transform.position, reflectBullet, enemy);
            _time = 0;
        }
    }
}
