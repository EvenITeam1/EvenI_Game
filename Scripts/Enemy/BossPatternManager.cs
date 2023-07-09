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

    public Transform relativePosition;

    bool ready = false;

    private void OnEnable() {
        SetBossBullet.relativePosition = this.relativePosition;
    }
    
    void Update()
    {
        if(!ready)
        {
            ready = true;
            StartCoroutine(executeRandomPattern(6));
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

                case 4:
                pattern5();
                break;

                case 5:
                pattern6();
                break;
            }
        ready = false;
    }

    void pattern1()
    {
        int n = Random.Range(0, 2);
        SetBossLaser.executeLaser(laserObj[n]);
    }

    public float interval2;
    void pattern2()//example
    {
        pattern2_sub1();
        Invoke("pattern2_sub2", interval2);
        Invoke("pattern2_sub3", interval2 * 2);
        Invoke("pattern2_sub4", interval2 * 3);
        Invoke("pattern2_sub5", interval2 * 4);
        Invoke("pattern2_sub6", interval2 * 5);
    }
    #region 패턴2 구성함수
    void pattern2_sub1()
    {
        float relativePositionX = relativePosition.position.x;
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 4), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 3), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 0), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, -1), basicBullet, enemy);
    }

    void pattern2_sub2()
    {
        float relativePositionX = relativePosition.position.x;
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 4), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 0), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, -2), basicBullet, enemy);
    }

    void pattern2_sub3()
    {
        float relativePositionX = relativePosition.position.x;
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 3), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, -1), basicBullet, enemy);
    }
    void pattern2_sub4()
    {
        float relativePositionX = relativePosition.position.x;
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 0), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, -2), basicBullet, enemy);
    }

    void pattern2_sub5()
    {
        float relativePositionX = relativePosition.position.x;
        SetBossBullet.fireBullet(new Vector2(relativePositionX, 1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(relativePositionX, -1), basicBullet, enemy);
    }

    void pattern2_sub6()
    {
        SetBossBullet.fireBullet(player.transform.position, basicBullet, enemy);
    }
    #endregion

    void pattern3()
    {
        SetBossBullet.fireBulletRandomPos(-3, 14, reflectBullet, enemy);
    }

    void pattern4()
    {
        float n = SetBossMove.goUp(5, moveSpeed, enemy, flag);
        Invoke("patternSub4", n);


        pattern2_sub6();
        for(int i = 1; i< 6; i++)
        Invoke("pattern2_sub6", 0.7f * i);     
    }
    #region 패턴4 구성함수
    void patternSub4()
    {
        SetBossMove.goDown(5, moveSpeed, enemy, flag);
    }
    #endregion


    public float interval5;
    void pattern5()
    {
        pattern5_sub1();
        for (int i = 1; i < 10; i++)
        {
            Invoke("pattern5_sub1", interval5 * i);
        }
    }
    #region 패턴5 구성함수
    void pattern5_sub1()
    {
        for (int i = 0; i < 6; i++)
        {
            SetBossBullet.fireBulletRandomPos(-3, 5, basicBullet, enemy);
        }
    }
    #endregion


    public float interval6;
    public float interval6_1;
    public float delay6;
    void pattern6()
    {
        float n1 = SetBossMove.goDown(1, moveSpeed, enemy, flag);
        Invoke("pattern6_sub2", n1 + interval6);
        Invoke("pattern6_sub2", (n1 + interval6) * 2);
        Invoke("pattern6_sub3", (n1 + interval6) * 3);

        for (int i = 0; i < 6; i++)
        {
            Invoke("pattern6_sub1", n1 + interval6_1 * i);
        }

        for (int i = 0; i < 6; i++)
        {
            Invoke("pattern6_sub1", (n1 + interval6) + interval6_1 * i + delay6);
        }

        for (int i = 0; i < 6; i++)
        {
            Invoke("pattern6_sub1", (n1 + interval6) * 2 + interval6_1 * i + delay6);
        }

        for (int i = 0; i < 6; i++)
        {
            Invoke("pattern6_sub1", (n1 + interval6) * 3 + interval6_1 * i + delay6);
        }
    }
    #region 패턴6 구성함수
    void pattern6_sub1()
    {
        SetBossBullet.fireBulletLocal(0, basicBullet, enemy);
    }

    void pattern6_sub2()
    {
        SetBossMove.goUp(1, moveSpeed, enemy, flag);
    }

    void pattern6_sub3()
    {
        SetBossMove.goDown(1, moveSpeed, enemy, flag);
    }
    #endregion
}
