using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage7BossPattern : MonoBehaviour
{
    float _time;
    [SerializeField] float coolTime;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject basicBullet;
    [SerializeField] GameObject basicBulletslow;
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
            executeRandomPattern(4).Forget();
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
                pattern4();
                break;

            case 3:
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
        float delay = 0.3f;
        for (int i = 0; i < 15; i++)
        {
            SetBossBullet.fireBullet(player.transform.position, basicBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            delay -= 0.02f;
        }
        patternActive = false;
    }

    void pattern2()
    {
        pattern2_shot().Forget();
    }

    async UniTaskVoid pattern2_shot()
    {
        animator.SetTrigger("Attack2");
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.9f));
        pattern2_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
        pattern2_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern2_sub3();
        patternActive = false;
    }
    void pattern2_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(9, -0.7f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -0.9f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.1f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.3f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.7f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.9f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.1f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.3f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.7f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.9f), basicBullet, enemy2);
    }
    void pattern2_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(8, -0.7f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.1f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.9f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -2.3f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -2.7f), basicBullet, enemy2);
    }
    void pattern2_sub3()
    {
        SetBossBullet.fireBullet(new Vector2(9, -0.6f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -0.8f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.2f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.4f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.6f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -1.8f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.2f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.4f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.6f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(9, -2.8f), basicBullet, enemy2);
    }
    /*void pattern3()
    {
        pattern3_shot().Forget();
    }
    async UniTaskVoid pattern3_shot()
    {
        animator.SetTrigger("Attack");
        await UniTask.Delay(TimeSpan.FromSeconds(0.6f));
        for (int i = 0; i < 7; i++)
        {
            SetBossBullet.fireBulletRandomPos(-10, 10, reflectBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(interval3));
        }
        patternActive = false;
    }*/

    void pattern4()
    {
        pattern4_shot().Forget();
    }
    async UniTaskVoid pattern4_shot()
    {
        animator.SetTrigger("Attack1");
        await UniTask.Delay(TimeSpan.FromSeconds(1.1f));
        pattern4_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        pattern4_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        pattern4_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        pattern4_sub2();
        patternActive = false;
    }
    void pattern4_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(8, -0.7f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.1f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.5f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.9f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -2.3f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -2.7f), basicBulletslow, enemy2);
    }
    void pattern4_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(8, -0.7f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.1f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.5f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -1.9f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -2.3f), basicBulletslow, enemy2);
        SetBossBullet.fireBullet(new Vector2(8, -2.7f), basicBulletslow, enemy2);

    }
    void pattern4_sub3()
    {
        SetBossBullet.fireBullet(new Vector2(8, 10.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 9.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 9.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 8.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 8.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 7.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 7.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 6.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 6.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 5.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 5.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 4.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 4.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 3.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 3.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 2.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 2.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 1.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 0.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 0.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, -0.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, -0.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, -1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, -1.75f), basicBullet, enemy);
    }
    void pattern4_sub4()
    {
        SetBossBullet.fireBullet(new Vector2(8, 10.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 9.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 9.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 8.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 8.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 7.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 7.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 6.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 6.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 5.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 5.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 4.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 4.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 3.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 3.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 2.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 2.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 1.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 0.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, 0.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, -0.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, -0.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, -1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(8, -1.75f), basicBullet, enemy);
    }

    void pattern6()
    {
        pattern6_move().Forget();
    }

    async UniTaskVoid pattern6_move()
    {
        animator.SetTrigger("Walk");
        await UniTask.Delay(TimeSpan.FromSeconds(1.75f));
        float n1 = SetBossMove.goLeft(22, moveSpeed, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n1));
        float n2 = SetBossMove.goRight(22, moveSpeed, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n2));
        patternActive = false;
    }
}