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
            int n = Random.Range(0, 2);
            SetEnemyLaser.executeLaser(_laserObj[n]);         
            _time = 0;
        }
    }
}
