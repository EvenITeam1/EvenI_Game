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
    private static GameManager _instance = null;
    public static GameManager Instance {
        get {
            if(_instance == null){
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
        set {
            _instance = value;
        }
    }
    public UnityEvent AfterInitializeEvent;
    public bool IsTestMode;
    public bool IsAsyncLoaded = false;

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);

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
            GlobalSoundManager ??= GetComponentInChildren<SoundManager>();

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

    [ContextMenu("테이블 데이터 모두 가져오기")]
    public async void LoadDataFromSheet()
    {
        await UniTask.WhenAll(
            CharacterDataTableDesign.DownloadItemSO(),
            ObjectDataTableDesign.DownloadItemSO(),
            BulletDataTableDesign.DownloadItemSO(),
            LaserDataTableDesign.DownloadItemSO(),
            MobDataTableDesign.DownloadItemSO(),
            BossDataTableDesign.DownloadItemSO()
        );
    }

    public Game_PL_Character_DataTable_design CharacterDataTableDesign;
    public Game_PL_Object_DataTable_design ObjectDataTableDesign;
    public Game_PL_Bullet_DataTable_design BulletDataTableDesign;
    public Game_PL_Laser_DataTable_design LaserDataTableDesign;
    public Game_PL_Mob_DataTable_design MobDataTableDesign;
    public Game_PL_Boss_DataTable_design BossDataTableDesign;
    public Game_PL_Tip_DataTable_design TipDataTableDesign;
    public SaveNLoadManager GlobalSaveNLoad;
    public SoundManager GlobalSoundManager;
}