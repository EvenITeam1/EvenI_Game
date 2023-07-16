using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEditor;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent AfterInitializeEvent;
    public bool IsTestMode;
    public bool IsAsyncLoaded = false;

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        if (IsTestMode == false)
        {
            CharacterDataTableDesign ??= GetComponentInChildren<Game_PL_Character_DataTable_design>();
            ObjectDataTableDesign ??= GetComponentInChildren<Game_PL_Object_DataTable_design>();
            BulletDataTableDesign ??= GetComponentInChildren<Game_PL_Bullet_DataTable_design>();
            LaserDataTableDesign ??= GetComponentInChildren<Game_PL_Laser_DataTable_design>();
            MobDataTableDesign ??= GetComponentInChildren<Game_PL_Mob_DataTable_design>();
            BossDataTableDesign ??= GetComponentInChildren<Game_PL_Boss_DataTable_design>();
            TipDataTableDesign ??= GetComponentInChildren<Game_PL_Tip_DataTable_design>();
            GlobalSaveNLoad ??= GetComponentInChildren<SaveNLoadManager>();

            if (!IsAsyncLoaded)
            {
                await UniTask.WhenAll(
                    CharacterDataTableDesign.DownloadItemSO(),
                    ObjectDataTableDesign.DownloadItemSO(),
                    BulletDataTableDesign.DownloadItemSO(),
                    LaserDataTableDesign.DownloadItemSO(),
                    MobDataTableDesign.DownloadItemSO(),
                    BossDataTableDesign.DownloadItemSO(),
                    TipDataTableDesign.DownloadItemSO()
                );
                IsAsyncLoaded = true;
            }
        }
        // 이렇게 하면 다음 scene으로 넘어가도 오브젝트가 사라지지 않습니다.
    }

    private void Start()
    {
        AfterInitializeEvent.Invoke();
    }

    public Game_PL_Character_DataTable_design CharacterDataTableDesign;
    public Game_PL_Object_DataTable_design ObjectDataTableDesign;
    public Game_PL_Bullet_DataTable_design BulletDataTableDesign;
    public Game_PL_Laser_DataTable_design LaserDataTableDesign;
    public Game_PL_Mob_DataTable_design MobDataTableDesign;
    public Game_PL_Boss_DataTable_design BossDataTableDesign;
    public Game_PL_Tip_DataTable_design TipDataTableDesign;
    public SaveNLoadManager GlobalSaveNLoad;
}