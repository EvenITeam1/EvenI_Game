using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage4BossPattern : MonoBehaviour
{
    float _time;
    [SerializeField] float coolTime;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject[] laserObj;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject flag;
    [SerializeField] GameObject slowBasicBullet;
    [SerializeField] GameObject fastBasicBullet;
    [SerializeField] BoxCollider2D laserPatternCollider;

    Animator animator;
    GameObject player;

    bool ready = false;
    bool patternActive = false;

    public float interval2;
    private void Awake()
    {
        player = RunnerManager.Instance.GlobalPlayer.gameObject;
        animator = enemy.GetComponent<Animator>();
        laserPatternCollider.enabled = false;
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
                laser().Forget();
                break;

            case 1:
                basicAttack().Forget();
                break;

            case 2:
                jumpAttack().Forget();
                break;

            case 3:
                vomit().Forget();
                break;
        }
        await UniTask.WaitUntil(() => patternActive == false);
        ready = false;
    }

    async UniTaskVoid laser()
    {
        SetBossLaser.executeLaser(laserObj[0]);
        await UniTask.Delay(TimeSpan.FromSeconds(0.75f));
        animator.SetTrigger("Laser");
        laserPatternCollider.enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(5.75f));
        laserPatternCollider.enabled = false;
        patternActive = false;
    }

    async UniTaskVoid basicAttack()
    {
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub2();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub1();
        await UniTask.Delay(TimeSpan.FromSeconds(interval2));
        pattern2_sub2();
        patternActive = false;
    }
    #region basicAttack_sub_Func
    void pattern2_sub1()
    {
        for (float i = 4; i > -3; i -= .5f)
        {
            SetBossBullet.fireBullet(new Vector2(8, i), slowBasicBullet, enemy);
        }
    }

    void pattern2_sub2()
    {
        for (float i = 3.75f; i > -3.25; i -= .5f)
        {
            SetBossBullet.fireBullet(new Vector2(8, i), slowBasicBullet, enemy);
        }
    }
    #endregion
    async UniTaskVoid jumpAttack()
    {
        int n = Random.Range(3, 6);
        for (int i = 0; i < n; i++)
        {
            animator.SetTrigger("Jump");
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            jump_shot();
            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        }

        patternActive = false;
    }

    void jump_shot()
    {
       int n = Random.Range(0, 2);

        switch(n)
        {
            case 0:
                SetBossBullet.fireBullet(new Vector2(8, 1f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.9f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.8f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.7f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.6f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.5f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.4f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.3f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.2f), fastBasicBullet, enemy);
                break;

            case 1:
                SetBossBullet.fireBullet(new Vector2(8, 1.2f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 1.1f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 1f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.9f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.8f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.7f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.6f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.5f), fastBasicBullet, enemy);
                SetBossBullet.fireBullet(new Vector2(8, 0.4f), fastBasicBullet, enemy);
             
                break;
        }
    }

    async UniTaskVoid vomit()
    {
        animator.SetTrigger("Vomit");
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        
        for (int i = 0; i < 5; i++)
        {
            SetBossBullet.fireBulletRandomPos(-3f, 0f, fastBasicBullet, enemy);
            SetBossBullet.fireBulletRandomPos(-3f, 0f, fastBasicBullet, enemy);
            SetBossBullet.fireBulletRandomPos(-3f, 0f, fastBasicBullet, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        patternActive = false;
    }

   
}
