using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobData {
    public static readonly int indexBasis = 4000;
    public  MOB_INDEX  Index;
    public  string Mob_name;
    public  int Mob_category;
    public  float Mob_width;
    public  float Mob_height;
    public  float Mob_hp;
    public  int Mob_bullet_index;
    public  float Mob_character;
    public  int Mob_movement;
    public  float Mob_speed;

    public MobData(){
        Index = MOB_INDEX.DEFAULT;
        Mob_name = "비어있음";
        Mob_category = 0;
        Mob_width = 1;
        Mob_height = 1;
        Mob_hp = 1;
        Mob_bullet_index = 0;
        Mob_character = 0;
        Mob_movement = 0;
        Mob_speed = 0;
    }

    public MobData(string _parsedLine){
        string[] datas = _parsedLine.Split('\t');

        Index = (MOB_INDEX)int.Parse(datas[0]);
        Mob_name = datas[1].Replace('_', ' ');
        Mob_category = int.Parse(datas[2]);
        Mob_width = float.Parse(datas[3]);
        Mob_height = float.Parse(datas[4]);
        Mob_hp = float.Parse(datas[5]);
        Mob_bullet_index = int.Parse(datas[6]);
        Mob_character = float.Parse(datas[7]);
        Mob_movement = int.Parse(datas[8]);
        Mob_speed = float.Parse(datas[9]);
    }
}

[System.Serializable]
public class MobMoveData {
    public Ease invokeEase;
    public float invokePosition;
    public float invokeEaseTime;

    public bool IsInfiniteLifetime;
    public float lifeTime;
    public float exitPosition;
    public float exitEaseTime;
    public Ease exitEase;

    public float GetLifeTime(){
        return invokeEaseTime + lifeTime;
    }
}