using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObjectData {
    public OBJECT_INDEX Index;
    public string Ob_name;
    public OBJECT_CATEGORY Ob_category;
    public int Ob_height;
    public int Ob_width;
    public int Ob_floor;
    public int Ob_damage;
    public bool Ob_destroy;

    public ObjectData(string _parsedLine){
        string[] datas = _parsedLine.Split(",");
        this.Index = (OBJECT_INDEX)int.Parse(datas[0]);
        this.Ob_name = datas[1];
        this.Ob_category = (OBJECT_CATEGORY)int.Parse(datas[2]);
        this.Ob_height = int.Parse(datas[3]);
        this.Ob_width = int.Parse(datas[4]);
        this.Ob_floor = int.Parse(datas[5]);
        this.Ob_damage = int.Parse(datas[6]);
        this.Ob_destroy = bool.Parse(datas[7]);
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