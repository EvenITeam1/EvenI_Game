using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public float interval1;
    public float interval2;
    public float interval4;
    public float interval5;
    public float interval6_move;
    public float interval6_shot;
    float arriveTime6;
    
    void Update()
    {
        if(!ready)
        {
            ready = true;
            executeRandomPattern(6).Forget();
        }
    }

    async UniTaskVoid executeRandomPattern(int patternN)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(coolTime));
        int n = Random.Range(0, patternN);

        switch (n)
        {
            case 0:
                pattern1();
                break;

            case 1:
                pattern2();
                break;

            case 2:
                pattern3();
                break;

            case 3:
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
        pattern1_shot().Forget();
    }

    async UniTaskVoid pattern1_shot()
    {
        for (int i = 0; i < 6; i++)
        {
            SetBossLaser.executeLaser(laserObj[i]);
            await UniTask.Delay(TimeSpan.FromSeconds(interval1));
        }   
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

    void pattern2()
    {
        pattern2_shot().Forget();
    }

    async UniTaskVoid pattern2_shot()
    {
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub4();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub5();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub6();
    }
    void pattern3()
    {
        SetBossBullet.fireBulletRandomPos(-10, 10, reflectBullet, enemy);
    }

    void pattern4()
    {
        pattern4_move().Forget();
        pattern4_shot().Forget();
    }

    async UniTaskVoid pattern4_move()
    {
        float n = SetBossMove.goUp(5, moveSpeed, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n));
        SetBossMove.goDown(5, moveSpeed, enemy, flag);
    }

    async UniTaskVoid pattern4_shot()
    {
        for (int i = 0; i < 7; i++)
        {
            SetBossBullet.fireBullet(player.transform.position, basicBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(interval4));
        }       
    }

    
    void pattern5()
    {
        pattern5_shot().Forget();
    }

    async UniTaskVoid pattern5_shot()
    {
        for(int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                SetBossBullet.fireBulletRandomPos(-5, 3, basicBullet, enemy);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(interval5));
        }      
    }

    void pattern6()
    {
        pattern6_move().Forget();
        pattern6_shot().Forget();
    }

    async UniTaskVoid pattern6_move()
    {
        float n = SetBossMove.goDown(1, moveSpeed, enemy, flag);
        arriveTime6 = n;
        await UniTask.Delay(TimeSpan.FromSeconds(n + interval6_move));
        SetBossMove.goUp(1, moveSpeed, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n + interval6_move));
        SetBossMove.goDown(1, moveSpeed, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n + interval6_move));
        SetBossMove.goUp(1, moveSpeed, enemy, flag);
    }

    async UniTaskVoid pattern6_shot()
    {
        for (int i = 0; i < 4; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(arriveTime6 + 0.1f));
            for (int j = 0; j < 8; j++)
            {
                SetBossBullet.fireBulletLocal(0, basicBullet, enemy);
                await UniTask.Delay(TimeSpan.FromSeconds(interval6_shot));
            }
        }      
    }
   
}
