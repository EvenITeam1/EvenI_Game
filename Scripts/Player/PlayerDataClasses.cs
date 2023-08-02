using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class PlayerData
{

    public static readonly int indexBasis = 1000;
    public DOG_INDEX Index;
    public string Character_korname;
    public string Character_engname;
    public float Character_width;
    public float Character_height;
    public BULLET_INDEX Character_bullet_index_1;
    public BULLET_INDEX Character_bullet_index_2;
    public float Character_hp;
    public float Character_per_hp_heal;
    public float Character_attack_speed;
    public int Character_skill_index;
    public int Character_cost;
    public string Character_script;
    public string Character_filecode;
    
    public PlayerData(){
        this.Index                      = DOG_INDEX.DEFAULT;
        this.Character_korname          = "비어있음";
        this.Character_engname          = "비어있음";
        this.Character_width            = 0;
        this.Character_height           = 0;
        this.Character_bullet_index_1   = BULLET_INDEX.DEFAULT;
        this.Character_bullet_index_2   = BULLET_INDEX.DEFAULT;
        this.Character_hp               = 0;
        this.Character_per_hp_heal      = 0;
        this.Character_attack_speed     = 0;
        this.Character_skill_index      = 0;
        this.Character_cost             = 0;
        this.Character_script           = "비어있는 데이터 입니다.";
        this.Character_filecode         = "";
    }
    
    public PlayerData(string _parsedLine)
    {
        string[] datas = _parsedLine.Trim().Split('\t');
        
        Index                           = (DOG_INDEX)int.Parse(datas[0]);
        Character_korname               = datas[1].Replace('_', ' ');
        Character_engname               = datas[2].Replace('_', ' ');
        Character_width                 = float.Parse(datas[3]);
        Character_height                = float.Parse(datas[4]);
        Character_bullet_index_1        = (BULLET_INDEX)int.Parse(datas[5]);
        Character_bullet_index_2        = (BULLET_INDEX)int.Parse(datas[6]);
        Character_hp                    = float.Parse(datas[7]);
        Character_per_hp_heal           = float.Parse(datas[8]);
        Character_attack_speed          = float.Parse(datas[9]);
        Character_skill_index           = int.Parse(datas[10]);
        Character_cost                  = int.Parse(datas[11]);
        Character_script                = datas[12].Replace('_', ' ');
        Character_filecode              = datas[13].Replace('_', ' ');
    }
}

[System.Serializable]
public class PlayerJumpData
{
    [field: SerializeField]
    public Transform[] groundCheckerTransform;
    public Transform[] ceilCheckerTransform;
    [field: SerializeField]
    public LayerMask groundLayer;

    public float jumpingPower = 16f;
    public int maxJumpCount = 3;
    public int jumpCount = 0;
    public bool isJumping = false;
    public bool IsActivatedOnce = false;

    public bool isAiring = false;
    public bool isAirHoldable = true;
    public bool isAirHoldPrevented = false;
    
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
    public bool IsSlowDownStart = false;
    public PlayerMoveData() { }
}

[System.Serializable]
public class PlayerVisualData
{
    [field: SerializeField] 
    public SpriteRenderer spriteRenderer;
    
    [field: SerializeField] 
    public Animator playerAnimator;
    //public Animator runningVFXAnimator;
    public PlayerVisualData() { }
}

[System.Serializable]
public class PlayerSoundData 
{
    public AudioClip JumpActive;
    public AudioClip GetDamaged;
    public AudioClip Revival;
    public PlayerSoundData() { }
}