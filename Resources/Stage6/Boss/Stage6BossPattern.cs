using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage6BossPattern : MonoBehaviour
{
    float _time;
    [SerializeField] float coolTime;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject[] laserObj; 
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject basicBullet;
    [SerializeField] GameObject reflectBullet;
    Animator animator;
    GameObject player;

    bool ready = false;
    bool patternActive = false;

    public float interval1;
    public float interval2;
    public float interval3;
    public float interval4;
    public float interval5;
    public float interval6;

    private void Awake()
    {
        player = RunnerManager.Instance.GlobalPlayer.gameObject;
        animator = enemy.GetComponent<Animator>();
    }
    void Update()
    {
        if (!ready)
        {
            ready = true;
            executeRandomPattern(6).Forget();
        }
    }

    async UniTaskVoid executeRandomPattern(int patternN)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(coolTime));
        int n = Random.Range(0, patternN);
        patternActive = true;
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
        await UniTask.WaitUntil(() => patternActive == false);
        ready = false;
    }

    void pattern1()
    {
        pattern1_shot().Forget();
    }

    async UniTaskVoid pattern1_shot()
    {
        for (int i = 0; i < 3; i++)
        {
            SetBossLaser.executeLaser(laserObj[i]);
            await UniTask.Delay(TimeSpan.FromSeconds(interval1));
        }
        SetBossLaser.executeLaser(laserObj[3]);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        SetBossLaser.executeLaser(laserObj[4]);
        patternActive = false;
    }

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
        patternActive = false;
    }
    #region 패턴2 구성함수
    void pattern2_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(7, 4), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 3), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -1), basicBullet, enemy);
    }

    void pattern2_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(7, 4), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -2), basicBullet, enemy);
    }

    void pattern2_sub3()
    {
        SetBossBullet.fireBullet(new Vector2(7, 1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -0.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -0.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -1.25f), basicBullet, enemy);
    }
    void pattern2_sub4()
    {
        SetBossBullet.fireBullet(new Vector2(7, 3), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -3), basicBullet, enemy);
    }

    void pattern2_sub5()
    {
        SetBossBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -2), basicBullet, enemy);
    }

    void pattern2_sub6()
    {
        SetBossBullet.fireBullet(new Vector2(7, 1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -0.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -1.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -2), basicBullet, enemy);
    }
    #endregion
    void pattern3()
    {
        pattern3_shot().Forget();
    }
    async UniTaskVoid pattern3_shot()
    {
        animator.SetTrigger("Attack");
        await UniTask.Delay(TimeSpan.FromSeconds(0.6f));
        for (int i = 0; i < 8; i++)
        {
            SetBossBullet.fireBulletRandomPos(-10, 10, reflectBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(interval3));
        }
        patternActive = false;
    }

    void pattern4()
    {
        pattern4_shot().Forget();
    }

    async UniTaskVoid pattern4_shot()
    {
        for (int i = 0; i < 20; i++)
        {
            SetBossBullet.fireBullet(player.transform.position, basicBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(interval4));
        }
        patternActive = false;
    }


    void pattern5()
    {
        pattern5_shot().Forget();
    }

    async UniTaskVoid pattern5_shot()
    {
        for (int i = 0; i < 7; i++)
        {
            SetBossBullet.fireBullet(player.transform.position, basicBullet, enemy);
            SetBossBullet.fireBullet(player.transform.position + new Vector3(0, 2, 0), basicBullet, enemy);
            SetBossBullet.fireBullet(player.transform.position + new Vector3(0, 1, 0), basicBullet, enemy);
            SetBossBullet.fireBullet(player.transform.position + new Vector3(0, -1, 0), basicBullet, enemy);
            SetBossBullet.fireBullet(player.transform.position + new Vector3(0, -2, 0), basicBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(interval5));
        }
        patternActive = false;
    }

    void pattern6()
    {
        pattern6_move().Forget();
    }

    async UniTaskVoid pattern6_move()
    {
        animator.SetBool("isWalking", true);
        float n1 = SetBossMove.goLeft(21, moveSpeed, enemy, flag);  
        await UniTask.Delay(TimeSpan.FromSeconds(n1));  
        float n2 = SetBossMove.goRight(21, moveSpeed, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n2));
        animator.SetBool("isWalking", false);
        patternActive = false;
    }
}
