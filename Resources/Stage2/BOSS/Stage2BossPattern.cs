using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage2BossPattern : MonoBehaviour
{
    float _time;
    [SerializeField] float coolTime;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject[] laserObj;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject basicBullet;
    [SerializeField] GameObject basicBulletslow;
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
    float arriveTime4;

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
            executeRandomPattern(3).Forget();
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
                pattern2();
                break;

            case 1:
                pattern3();
                break;

            case 2:
                pattern4();
                break;
        }
        await UniTask.WaitUntil(() => patternActive == false);
        ready = false;
    }

    void pattern2()
    {
        pattern2_shot().Forget();
    }

    async UniTaskVoid pattern2_shot()
    {
        animator.SetTrigger("attack");
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        pattern2_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        pattern2_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(4));
        patternActive = false;
    }
    #region ����2 �����Լ� 
    void pattern2_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(-10, -1.5f), basicBulletslow, enemy);
        SetBossBullet.fireBullet(new Vector2(-10, 2.5f), basicBulletslow, enemy);
        SetBossBullet.fireBullet(new Vector2(-10, 5.5f), basicBulletslow, enemy);
        SetBossBullet.fireBullet(new Vector2(-10, 8.5f), basicBulletslow, enemy);
        SetBossBullet.fireBullet(new Vector2(-10, 11.5f), basicBulletslow, enemy);
    }

    void pattern2_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(-10, -2), basicBulletslow, enemy);
        SetBossBullet.fireBullet(new Vector2(-10, 1), basicBulletslow, enemy);
        SetBossBullet.fireBullet(new Vector2(-10, 4), basicBulletslow, enemy);
        SetBossBullet.fireBullet(new Vector2(-10, 7), basicBulletslow, enemy);
        SetBossBullet.fireBullet(new Vector2(-10, 10), basicBulletslow, enemy);
    }

    #endregion
    void pattern3()
    {
        pattern3_shot().Forget();
    }
    async UniTaskVoid pattern3_shot()
    {
        animator.SetTrigger("reflect");
        await UniTask.Delay(TimeSpan.FromSeconds(2.3f));
        for (int i = 0; i < 2; i++)
        {
            SetBossBullet.fireBulletRandomPos(-10, 10, reflectBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(4));
        patternActive = false;
    }

    void pattern4()
    {
        pattern4_move().Forget();
        pattern4_shot().Forget();
    }

    async UniTaskVoid pattern4_move()
    {
        float n = SetBossMove.goDown(4, moveSpeed, enemy, flag);
        arriveTime4 = n;
        await UniTask.Delay(TimeSpan.FromSeconds(n + 0.7f));
        SetBossMove.goUp(4, moveSpeed, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n + 0.7f));
        SetBossMove.goDown(4, moveSpeed, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n + 0.7f));
        SetBossMove.goUp(4, moveSpeed, enemy, flag);
    }

    async UniTaskVoid pattern4_shot()
    {
        for (int i = 0; i < 4; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(arriveTime4 + 0.01f));
            for (int j = 0; j < 5; j++)
            {
                SetBossBullet.fireBullet(player.transform.position, basicBullet, enemy);
                await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
            }
        }
        patternActive = false;
    }

}