using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null) Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake() {
        this.GlobalPlayer ??= GameObject.FindGameObjectWithTag(GlobalStrings.LAYERS_STRING[(int)PROJECT_LAYERS.Player]).GetComponent<Player>();
    }

    public Game_PL_Character_DataTable_design       CharacterDataTableDesign;
    public Game_PL_Object_DataTable_design          ObjectDataTableDesign;
    public Game_PL_Bullet_DataTable_design          BulletDataTableDesign;
    public Game_PL_Laser_DataTable_design           LaserDataTableDesign;
    public Game_PL_Mob_DataTable_design             MobDataTableDesign;
    
    public GlobalEvent      GlobalEventInstance;

    public Player           GlobalPlayer;
    public MobGenerator     GlobalMobGenerator;
}