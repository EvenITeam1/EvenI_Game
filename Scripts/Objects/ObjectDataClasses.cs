using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObjectData {
    public static readonly int indexBasis = 2000;
    
    public OBJECT_INDEX Index;
    public string Ob_name;
    public OBJECT_CATEGORY Ob_category;
    public float Ob_width;
    public float Ob_height;
    public float Ob_damage;
    public int Ob_bullet_index;
    public MOVEMENT_INDEX Ob_movement_index;
    public float Ob_move_strength;
    public bool  OB_Hitable;
    public float Ob_HP;
    public int Ob_Score;
    public string Ob_filecode;

    public ObjectData()
    {
        this.Index              = OBJECT_INDEX.DEFAULT;
        this.Ob_name            = "비어있음";
        this.Ob_category        = OBJECT_CATEGORY.DEFAULT;
        this.Ob_width           = 0;
        this.Ob_height          = 0;
        this.Ob_damage          = 0;
        this.Ob_bullet_index    = -1;
        this.Ob_movement_index  = MOVEMENT_INDEX.HOLD;
        this.Ob_move_strength   = 0;
        this.OB_Hitable         = false;
        this.Ob_HP              = 1;
        this.Ob_Score           = 0;
        this.Ob_filecode        = "";
    }

    public ObjectData(string _parsedLine){
        string[] datas = _parsedLine.Trim().Split('\t');

        this.Index              = (OBJECT_INDEX)int.Parse(datas[0]);
        this.Ob_name            = datas[1].Replace('_', ' ');
        this.Ob_category        = (OBJECT_CATEGORY)int.Parse(datas[2]);
        this.Ob_width           = float.Parse(datas[3]);
        this.Ob_height          = float.Parse(datas[4]);
        this.Ob_damage          = int.Parse(datas[5]);
        this.Ob_bullet_index    = int.Parse(datas[6]);
        this.Ob_movement_index  = (MOVEMENT_INDEX)int.Parse(datas[7]);
        this.Ob_move_strength   = float.Parse(datas[8]);
        this.OB_Hitable         = int.Parse(datas[9]) == 1 ? true : false;
        this.Ob_HP              = float.Parse(datas[10]);
        this.Ob_Score           = int.Parse(datas[11]);
        this.Ob_filecode        = datas[12].Replace('_', ' ');
    }
}
[System.Serializable]
public class ObjectCoinData {
    public float ScoreValue;
    public ParticleSystem particle;
    public ObjectCoinData(){
        
    }
}

[System.Serializable]
public class ObjectItemData {
    string tmp;
    public ObjectItemData(){}
}

[System.Serializable]
public class ObjectVisualData {
    public SpriteRenderer   spriteRenderer;
    public Animator         animator;
    public Color            defaultColor;
    public Color            onHitColor;
}