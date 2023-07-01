using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    float _time;
    [SerializeField] float _coolTime;

    [SerializeField] GameObject[] _laserObj;
    
    void Update()
    {
        if(_time < _coolTime)
        {
            _time += Time.deltaTime;
        }

        else
        {
            SetEnemyLaser.instance.executeLaser(_laserObj[0]);
            _time = 0;
        }
    }
}
