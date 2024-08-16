using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class Stage8BossPattern : MonoBehaviour
{
    float _time;
    [SerializeField] float coolTime;
    [SerializeField] float moveSpeed1;
    [SerializeField] float moveSpeed2;
    [SerializeField] float moveSpeed3;
    [SerializeField] GameObject[] laserObj;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] GameObject enemy5;
    [SerializeField] GameObject enemy6;
    [SerializeField] GameObject enemy7;
    [SerializeField] GameObject enemy8;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject basicBullet;
    [SerializeField] GameObject basicBulletslow;
    [SerializeField] GameObject basicBulletslow2;
    [SerializeField] GameObject basicBulletshockwave;
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
            executeRandomPattern(5).Forget();
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
                pattern4();
                break;

            case 2:
                pattern6();
                break;

            case 3:
                pattern7();
                break;

            case 4:
                pattern8();
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
        animator.SetTrigger("Jump");
        await UniTask.Delay(TimeSpan.FromSeconds(0.9f));
        float n = SetBossMove.goUp(7, moveSpeed1, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
        arriveTime4 = n;
        pattern1_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern1_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern1_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern1_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern1_sub1();
        float n2 = SetBossMove.goDown(7, moveSpeed1, enemy, flag);
        patternActive = false;
    }
    void pattern1_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(0, 7.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(1, 6), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(2, 4.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(3, 3), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(4, 1.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 0), basicBullet, enemy);
    }
    void pattern1_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(0, 8.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(1, 6.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(2, 5.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(3, 3.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(4, 2.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 0.75f), basicBullet, enemy);
    }

    void pattern4()
    {
        pattern4_shot().Forget();
    }
    async UniTaskVoid pattern4_shot()
    {
        animator.SetTrigger("Attack2");
        await UniTask.Delay(TimeSpan.FromSeconds(0.95f));
        pattern4_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        pattern4_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.85f));
        pattern4_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        pattern4_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        pattern4_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        pattern4_sub3();
        patternActive = false;
    }
    void pattern4_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(-3.5f, -4), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.5f, -2), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.5f, 0), basicBullet, enemy2);
    }
    void pattern4_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(-3.5f, 3), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.5f, 1), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.5f, -1), basicBullet, enemy2);
    }
        void pattern4_sub3()
    {
        SetBossBullet.fireBullet(new Vector2(-3.5f, 4), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.5f, 2), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.5f, 0), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.5f, -4), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.5f, -2), basicBullet, enemy2);
    }

    void pattern6()
    {
        pattern6_move().Forget();
    }

    async UniTaskVoid pattern6_move()
    {
        animator.SetTrigger("Walk");
        await UniTask.Delay(TimeSpan.FromSeconds(0.65f));
        float n1 = SetBossMove.goLeft(35, moveSpeed2, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n1 + 0.01f));
        SetBossMove.teleport(new Vector2(29.18f, 1.23f), enemy);
        float n2 = SetBossMove.goLeft(20, moveSpeed3, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n2 + 0.01f));
        patternActive = false;
    }

    void pattern7()
    {
        pattern7_shot().Forget();
    }
    async UniTaskVoid pattern7_shot()
    {
        animator.SetTrigger("Attack1");
        await UniTask.Delay(TimeSpan.FromSeconds(0.9f));
        pattern7_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        pattern7_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        pattern7_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        pattern7_sub4();
        await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        pattern7_sub5();
        patternActive = false;
    }

    void pattern7_sub1()
    {
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow, enemy4);
    }
    void pattern7_sub2()
    {
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow, enemy5);
    }
    void pattern7_sub3()
    {
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow, enemy6);
    }
    void pattern7_sub4()
    {
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow, enemy7);
    }
    void pattern7_sub5()
    {
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow, enemy8);
    }
    void pattern8()
    {
        pattern8_shot().Forget();
    }
    async UniTaskVoid pattern8_shot()
    {
        animator.SetTrigger("Attack3");
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        patternActive = false;
    }
    void pattern8_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(14, 9.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 8.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 8.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 7.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 7.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 6.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 6.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 5.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 5.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 4.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 4.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 3.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 3.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 2.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 2.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 1.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 1.13f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 0.63f), basicBullet, enemy3);
        SetBossBullet.fireBullet(new Vector2(14, 0.13f), basicBullet, enemy3);
    }

}

