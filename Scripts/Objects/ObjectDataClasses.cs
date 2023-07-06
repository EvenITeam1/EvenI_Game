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
    public int Ob_height;
    public int Ob_width;
    public int Ob_floor;
    public int Ob_damage;
    public bool Ob_destroy;

    public ObjectData(string _parsedLine){
        string[] datas = _parsedLine.Split(",");

        this.Index = IntToINDEX(int.Parse(datas[0]));
        this.Ob_name = datas[1].Replace('_', ' ');;
        this.Ob_category = (OBJECT_CATEGORY)int.Parse(datas[2]);
        this.Ob_height = int.Parse(datas[3]);
        this.Ob_width = int.Parse(datas[4]);
        this.Ob_floor = int.Parse(datas[5]);
        this.Ob_damage = int.Parse(datas[6]);
        this.Ob_destroy = bool.Parse(datas[7]);
    }

    public static OBJECT_INDEX IntToINDEX(int _input){
        int res = (_input - indexBasis);
        if(res <= 0) {throw new Exception("인덱스가 음수임");}
        return (OBJECT_INDEX)res;
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