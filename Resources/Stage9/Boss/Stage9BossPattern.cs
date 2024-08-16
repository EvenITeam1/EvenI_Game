using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class Stage9BossPattern : MonoBehaviour
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
        animator.SetTrigger("Attack2");
        float n = SetBossMove.goUp(5, moveSpeed1, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        arriveTime4 = n;
        SetBossLaser.executeLaser(laserObj[0]);
        await UniTask.Delay(TimeSpan.FromSeconds(n + 0.7f));
        float n2 = SetBossMove.goDown(11, moveSpeed1, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n2 + 0.7f));
        animator.SetTrigger("Attack2");
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        SetBossLaser.executeLaser(laserObj[1]);
        await UniTask.Delay(TimeSpan.FromSeconds(2.3f));
        SetBossMove.goUp(6, moveSpeed1, enemy, flag);
        patternActive = false;
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
        animator.SetTrigger("Attack2");
        await UniTask.Delay(TimeSpan.FromSeconds(0.35f));
        pattern4_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        pattern4_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        pattern4_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        pattern4_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        pattern4_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        pattern4_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        pattern4_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
        pattern4_sub1();
        patternActive = false;
    }
    void pattern4_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(5, 9.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 8.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 8), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 7.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 6.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 5.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 5), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 4.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 3.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 2.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 2), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 1.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 0.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -0.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -1), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -1.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -2.5f), basicBulletslow2, enemy3);
    }
    void pattern4_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(5, 9.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 8.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 7.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 7), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 6.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 5.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 4.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 4), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 3.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 2.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 1.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 1), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 0.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -0.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -1.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -2), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -2.75f), basicBulletslow2, enemy3);
    }

    void pattern4_sub3()
    {
        SetBossBullet.fireBullet(new Vector2(5, 9.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 9), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 8.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 7.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 6.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 6), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 5.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 4.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 3.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 3), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 2.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 1.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 0.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, 0), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -0.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -1.5f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -2.25f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -2.75f), basicBulletslow2, enemy3);
        SetBossBullet.fireBullet(new Vector2(5, -3.5f), basicBulletslow2, enemy3);
    }

    void pattern6()
    {
        pattern6_move().Forget();
    }

    async UniTaskVoid pattern6_move()
    {
        animator.SetTrigger("Walk");
        await UniTask.Delay(TimeSpan.FromSeconds(1.75f));
        float n1 = SetBossMove.goLeft(35, moveSpeed2, enemy, flag);
        await UniTask.Delay(TimeSpan.FromSeconds(n1 + 0.01f));
        SetBossMove.teleport(new Vector2(33.18f, 2.44f), enemy);
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
        var enemyHpScript = enemy.GetComponent<BossHP>();
        animator.SetTrigger("Attack1");
        await UniTask.Delay(TimeSpan.FromSeconds(0.6f));
        pattern7_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(3.2f));
        pattern7_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern7_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern7_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern7_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(0.65f));
        pattern7_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern7_sub2();
        if (enemyHpScript.getHP() < 4500)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.65f));
            pattern7_sub3();
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            pattern7_sub3();
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            pattern7_sub3();
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            pattern7_sub3();
        }
        patternActive = false;
    }

    void pattern7_sub1()
    {
        SetBossBullet.fireBulletLocal(0, basicBulletslow, enemy);
    }

    void pattern7_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(-3.25f, 2), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 3), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 4), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 5), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 6), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 7), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 1), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 0), basicBullet, enemy2);
    }

    void pattern7_sub3()
    {
        SetBossBullet.fireBullet(new Vector2(-3.25f, 1.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 2.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 3.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 4.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 5.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 6.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 7.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, 0.5f), basicBullet, enemy2);
        SetBossBullet.fireBullet(new Vector2(-3.25f, -0.5f), basicBullet, enemy2);
    }

    void pattern8()
    {
        pattern8_shot().Forget();
    }
    async UniTaskVoid pattern8_shot()
    {
        animator.SetTrigger("Attack2");
        await UniTask.Delay(TimeSpan.FromSeconds(0.35f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow2, enemy3);
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow2, enemy3);
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow2, enemy3);
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        SetBossBullet.fireBullet(player.transform.position, basicBulletslow2, enemy3);
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
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        pattern8_sub1();

        patternActive = false;
    }
    void pattern8_sub1()
    {
        var enemyHpScript = enemy.GetComponent<BossHP>();

        if(enemyHpScript.getHP() < 4500)
        {
            SetBossBullet.fireBullet(new Vector2(0, 9.5f), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 8), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 6.5f), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 5.0f), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 3.5f), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 2), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 0.5f), basicBullet, enemy3);
        }

        else
        {
            SetBossBullet.fireBullet(new Vector2(0, 9.5f), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 8), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 6.5f), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 5), basicBullet, enemy3);

            SetBossBullet.fireBullet(new Vector2(0, 2), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, 0.5f), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, -1), basicBullet, enemy3);
            SetBossBullet.fireBullet(new Vector2(0, -2.5f), basicBullet, enemy3);
        }     
    }
}

