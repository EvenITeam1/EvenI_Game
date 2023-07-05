using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    float _time;
    [SerializeField] float coolTime;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject[] laserObj;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject basicBullet;
    [SerializeField] GameObject reflectBullet;

    bool ready = false;
    
    void Update()
    {
        if(!ready)
        {
            ready = true;
            StartCoroutine(executeRandomPattern(4));
        }
    }

    IEnumerator executeRandomPattern(int PatternN)
    {
        yield return new WaitForSeconds(coolTime);

        int n = Random.Range(0, PatternN);

            switch (n)
            {
                case 0:
                    pattern1();
                    break;

                case 1:
                    pattern2();
                    break;

                case 2 :
                    pattern3();
                    break;

                case 3 :
                pattern4();
                break;

            }
        ready = false;
    }

    void pattern1()//example : ���� ������ ������Ʈ�� 2�� �־��� �ִ� ��Ȳ����, 0�� 1�� �߿� �������� �ϳ��� �������� ��� ����
    {
        int n = Random.Range(0, 2);
        SetEnemyLaser.executeLaser(laserObj[n]);
    }

    void pattern3()
    {
        int n = Random.Range(-3, 7);
        SetEnemyBullet.fireBullet(new Vector2(7, n), reflectBullet, enemy);   
    }

    public float _pattern2SubCool;

    void pattern2()//example
    {
        pattern2_sub1();
        Invoke("pattern2_sub2", _pattern2SubCool);
        Invoke("pattern2_sub3", _pattern2SubCool * 2);
        Invoke("pattern2_sub4", _pattern2SubCool * 3);
        Invoke("pattern2_sub5", _pattern2SubCool * 4);
        Invoke("pattern2_sub6", _pattern2SubCool * 5);
    }

    void pattern2_sub1()
    {
        SetEnemyBullet.fireBullet(new Vector2(7, 4), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 3), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 1), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 0), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, -1), basicBullet, enemy);
    }

    void pattern2_sub2()
    {
        SetEnemyBullet.fireBullet(new Vector2(7, 4), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 0), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
    }

    void pattern2_sub3()
    {
        SetEnemyBullet.fireBullet(new Vector2(7, 3), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 1), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, -1), basicBullet, enemy);
    }
    void pattern2_sub4()
    {
        SetEnemyBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, 0), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, -2), basicBullet, enemy);
    }

    void pattern2_sub5()
    {
        SetEnemyBullet.fireBullet(new Vector2(7, 1), basicBullet, enemy);
        SetEnemyBullet.fireBullet(new Vector2(7, -1), basicBullet, enemy);
    }

    void pattern2_sub6()
    {
        SetEnemyBullet.fireBullet(player.transform.position, basicBullet, enemy);
    }


    void pattern4()
    {
        float n = SetEnemyMove.goUp(5, moveSpeed, enemy, flag);
        pattern2_sub6();
        for(int i = 1; i< 6; i++)
        Invoke("pattern2_sub6", 0.7f * i);

        Invoke("patternSub4", n);
    }

    void patternSub4()
    {
        SetEnemyMove.goDown(5, moveSpeed, enemy, flag);
    }
}
