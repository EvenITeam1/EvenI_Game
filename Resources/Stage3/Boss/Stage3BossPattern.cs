using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage3BossPattern : MonoBehaviour
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
            executeRandomPattern(4).Forget();
        }
    }

    async UniTaskVoid executeRandomPattern(int patternN)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(coolTime));
        if (!gameObject.activeSelf)
            return;
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

            case 3:
                pattern5();
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
        animator.SetTrigger("Attackfast");
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        pattern2_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        pattern2_sub3();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        pattern2_sub4();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        patternActive = false;
    }
    #region ����2 �����Լ� 
    void pattern2_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(7, 2.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1.5f), basicBullet, enemy);
    }

    void pattern2_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(7, 1.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0.75f), basicBullet, enemy);
    }
    void pattern2_sub3()
    {
        SetBossBullet.fireBullet(new Vector2(7, 3.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 3), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.25f), basicBullet, enemy);
    }

    void pattern2_sub4()
    {
        SetBossBullet.fireBullet(new Vector2(7, 5), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 4.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 3.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -0.25f), basicBullet, enemy);
    }

    #endregion
    void pattern3()
    {
        pattern3_shot().Forget();
    }
    async UniTaskVoid pattern3_shot()
    {
        animator.SetTrigger("Attack");
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        for (int i = 0; i < 5; i++)
        {
            SetBossBullet.fireBullet(player.transform.position, basicBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
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

    void pattern5()
    {
        pattern5_shot().Forget();
    }

    async UniTaskVoid pattern5_shot()
    {
        animator.SetTrigger("Attackfast2");
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        pattern5_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern5_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern5_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern5_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
        pattern5_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        patternActive = false;
    }
    #region ����2 �����Լ� 
    void pattern5_sub1()
    {
        SetBossBullet.fireBullet(new Vector2(5, 5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 4.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 4.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 4.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 4), basicBullet, enemy);
    }

    void pattern5_sub2()
    {
        SetBossBullet.fireBullet(new Vector2(5, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 1.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 1.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(5, 1), basicBullet, enemy);
    }
    void pattern5_sub3()
    {
        SetBossBullet.fireBullet(new Vector2(7, 3.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 3), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.25f), basicBullet, enemy);
    }

    void pattern5_sub4()
    {
        SetBossBullet.fireBullet(new Vector2(7, 5), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 4.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 3.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2.75f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 2), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 1.25f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, 0.5f), basicBullet, enemy);
        SetBossBullet.fireBullet(new Vector2(7, -0.25f), basicBullet, enemy);
    }
}
#endregion