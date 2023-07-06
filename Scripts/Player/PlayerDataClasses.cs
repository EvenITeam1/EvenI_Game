using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public static readonly int indexBasis = 1000;
    public DOG_INDEX Index;
    public string Character_korname;
    public string Character_engname;
    public int Character_type;
    public float Character_damage;
    public float Maxdamage;
    public float Mindamage;
    public float Character_critical_prob;
    public float Character_critical_damage;
    public float Character_hp;
    public float Character_per_hp_heal;
    public float Character_attack_speed;
    public int Character_skill;
    public int Character_cost;
    public string Character_expl;
    
    public PlayerData(){
        Index = DOG_INDEX.DEFAULT;
        Character_korname = "비어있음";
        Character_engname = "비어있음";
        Character_type = 0;
        Character_damage = 0;
        Maxdamage = 0;
        Mindamage = 0;
        Character_critical_prob = 0;
        Character_critical_damage = 0;
        Character_hp = 0;
        Character_per_hp_heal = 0;
        Character_attack_speed = 0;
        Character_skill = 0;
        Character_cost = 0;
        Character_expl = "비어있는 데이터 입니다.";

    }
    public PlayerData(string _parsedLine)
    {
        string[] datas = _parsedLine.Split('\t');
        
        this.Index = (DOG_INDEX)int.Parse(datas[0]);
        this.Character_korname = datas[1].Replace('_', ' ');
        this.Character_engname = datas[2].Replace('_', ' ');
        this.Character_type = int.Parse(datas[3]);
        this.Character_damage = float.Parse(datas[4]);
        this.Maxdamage = float.Parse(datas[5]);
        this.Mindamage = float.Parse(datas[6]);
        this.Character_critical_prob = float.Parse(datas[7]);
        this.Character_critical_damage = float.Parse(datas[8]);
        this.Character_hp = float.Parse(datas[9]);
        this.Character_per_hp_heal = float.Parse(datas[10]);
        this.Character_attack_speed = float.Parse(datas[11]);
        this.Character_skill = int.Parse(datas[12]);
        this.Character_cost = int.Parse(datas[13]);
        this.Character_expl = datas[14].Replace('_', ' ');
    }
}

[System.Serializable]
public class PlayerJumpData
{
    [field: SerializeField]
    public Transform groundCheckerTransform;
    [field: SerializeField]
    public LayerMask groundLayer;

    public float jumpingPower = 16f;
    public int maxJumpCount = 3;
    public int jumpCount = 0;
    public bool isJumping = false;
    public bool IsActivatedOnce = false;
    public bool isAiring = false;
    public bool isAirHoldable = true;
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;
    public PlayerJumpData() {
    }
}

[System.Serializable]
public class PlayerMoveData
{
    public float horizontal = 1;
    public float speed = 10f;

    public PlayerMoveData() { }
}

[System.Serializable]
public class PlayerVisualData
{
    [field: SerializeField] 
    public SpriteRenderer spriteRenderer;
    
    [field: SerializeField] 
    public Animator animator;
    public PlayerVisualData() { }
}
