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
        this.GlobalPlayer ??= GameObject.FindGameObjectWithTag(GlobalStrings.PLAYER_STRING).GetComponent<Player>();
    }

    public Game_PL_Character_DataTable_design CharacterDataTableDesign;
    public Game_PL_Object_DataTable_design ObjectDataTableDesign;
    public GlobalEvent GlobalEventInstance;

    public Player GlobalPlayer;
    public MobGenerator GlobalMobGenerator;
}