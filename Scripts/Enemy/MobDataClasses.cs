using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MobData {
    
    
    public static readonly int indexBasis = 4000;
    public MOB_INDEX  Index;
    public string Mob_name;
    public int Mob_category;
    public float Mob_width;
    public float Mob_height;
    public float Mob_damage;
    public float Mob_hp;
    public BULLET_INDEX Mob_bullet_index;
    public bool Mob_tracking;
    public MOVEMENT_INDEX Mob_movement_index;
    public float Mob_move_strength;
    public int Mob_Score;
    public string Mob_filecode;

    public MobData(){
        this.Index                  = MOB_INDEX.DEFAULT;
        this.Mob_name               = "비어있음";
        this.Mob_category           = 0;
        this.Mob_width              = 1;
        this.Mob_height             = 1;
        this.Mob_hp                 = 1;
        this.Mob_bullet_index       = 0;
        this.Mob_tracking           = false;
        this.Mob_movement_index     = MOVEMENT_INDEX.HOLD;
        this.Mob_move_strength      = 0;
        this.Mob_Score              = 0;
        this.Mob_filecode           = "";
    }

    public MobData(string _parsedLine){
        string[] datas = _parsedLine.Trim().Split('\t');

        this.Index                  = (MOB_INDEX)int.Parse(datas[0]);
        this.Mob_name               = datas[1].Replace('_', ' ');
        this.Mob_category           = int.Parse(datas[2]);
        this.Mob_width              = float.Parse(datas[3]);
        this.Mob_height             = float.Parse(datas[4]);
        this.Mob_damage             = int.Parse(datas[5]);
        this.Mob_hp                 = float.Parse(datas[6]);
        this.Mob_bullet_index       = (BULLET_INDEX)int.Parse(datas[7]);
        this.Mob_tracking           = int.Parse(datas[8]) == 1 ? true : false;
        this.Mob_movement_index     = (MOVEMENT_INDEX)int.Parse(datas[9]);
        this.Mob_move_strength      = float.Parse(datas[10]);
        this.Mob_Score              = int.Parse(datas[11]);
        this.Mob_filecode           = datas[12].Replace('_', ' ');
    }
}

[System.Serializable]
public class MobMoveData {
    public bool IsInfiniteLifetime = false;
    public float lifeTime = 10f;

    public Vector2 invokePosition;
    public Vector2 exitPosition;
    public float invokeEaseTime = 2f;
    public float exitEaseTime = 3f;
    
    [HideInInspector] public Ease invokeEase = Ease.OutBack;
    [HideInInspector] public Ease exitEase = Ease.InBack;

    public List<UnityAction> moveType = new List<UnityAction>();

    public MobMoveData() {
        IsInfiniteLifetime = false;
        lifeTime = 10f;
        invokeEaseTime = 2f;
        exitEaseTime = 3f;
        invokeEase = Ease.OutBack;
        exitEase = Ease.InBack;
    }
}

[System.Serializable]
public class MobGenData {
    [SerializeField] public Mob[] mobs;
    [SerializeField] public MobMoveData[] mobMoveData;
    public float gap;
}