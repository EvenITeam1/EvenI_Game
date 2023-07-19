using UnityEngine;

#region Bullet
[System.Serializable]
public class BulletData {
    public static readonly int indexBasis = 3000;
    public BULLET_INDEX Index;
    public string Bullet_name;
    public BULLET_CATEGORY Bullet_category;
    public float Bullet_width;
    public float Bullet_height;
    public float Bullet_min_dmg;
    public float Bullet_max_dmg;
    public float Bullet_set_dmg;
    public float Bullet_crit_chance;
    public float Bullet_crit_const;
    public float Bullet_speed;
    public float Bullet_last_time;
    public int Bullet_reflect_count;
    public string Bullet_filecode;

    public BulletData()
    {
        this.Index                  = BULLET_INDEX.DEFAULT;
        this.Bullet_name            = "비어있음";
        this.Bullet_category        = BULLET_CATEGORY.DEFAULT;
        this.Bullet_width           = 0;
        this.Bullet_height          = 0;
        this.Bullet_min_dmg         = 0;
        this.Bullet_max_dmg         = 0;
        this.Bullet_set_dmg         = 0;
        this.Bullet_crit_chance     = 0;
        this.Bullet_crit_const      = 0;
        this.Bullet_speed           = 0;
        this.Bullet_last_time       = 0;
        this.Bullet_reflect_count   = 0;
        this.Bullet_filecode        = "";
    }

    public BulletData(string _parsedLine){
        string[] datas = _parsedLine.Trim().Split('\t');

        this.Index                  = (BULLET_INDEX)int.Parse(datas[0]);
        this.Bullet_name            = datas[1].Replace('_', ' ');
        this.Bullet_category        = (BULLET_CATEGORY)int.Parse(datas[2]);
        this.Bullet_width           = float.Parse(datas[3]);
        this.Bullet_height          = float.Parse(datas[4]);
        this.Bullet_min_dmg         = float.Parse(datas[5]);
        this.Bullet_max_dmg         = float.Parse(datas[6]);
        this.Bullet_set_dmg         = float.Parse(datas[7]);
        this.Bullet_crit_chance     = float.Parse(datas[8]);
        this.Bullet_crit_const      = float.Parse(datas[9]);
        this.Bullet_speed           = float.Parse(datas[10]);
        this.Bullet_last_time       = float.Parse(datas[11]);
        this.Bullet_reflect_count   = int.Parse(datas[12]);
        this.Bullet_filecode        = datas[13].Replace('_', ' ');
    }
}

[System.Serializable]
public class BulletVisualData
{
    public GameObject dmgPrefab;
    public int critFontSize;
    public int nonCritFontSize;
    public Color32 critColor;
    public Color32 nonCritColor;
}
#endregion /////////////////////////////////////////////////////////////////////////////////

#region Laser

[System.Serializable]
public class LaserData {
    public static readonly int indexBasis = 3100;
    public LASER_INDEX Index;
    public string Laser_name;
    public LASER_CATEGORY Laser_category;
    public float Laser_height;
    public float Laser_damage;
    public float Laser_last_time;
    public float Laser_movement_speed;
    public string Laser_filecode;

    public LaserData(){
        this.Index                      = LASER_INDEX.DEFAULT;
        this.Laser_name                 = "비어있음";
        this.Laser_category             = LASER_CATEGORY.DEFAULT;
        this.Laser_height               = 0;
        this.Laser_damage               = 0;
        this.Laser_last_time            = 0;
        this.Laser_movement_speed       = 0;
        this.Laser_filecode             = "";
    }

    public LaserData(string _parsedLine) {
        string[] datas = _parsedLine.Trim().Split('\t');

        this.Index                      = (LASER_INDEX)int.Parse(datas[0]);
        this.Laser_name                 = datas[1].Replace('_', ' ');
        this.Laser_category             = (LASER_CATEGORY)int.Parse(datas[2]);
        this.Laser_height               = float.Parse(datas[3]);
        this.Laser_damage               = float.Parse(datas[4]);
        this.Laser_last_time            = float.Parse(datas[5]);
        this.Laser_movement_speed       = float.Parse(datas[6]);
        this.Laser_filecode             = datas[7].Replace('_', ' ');
    }
}

#endregion /////////////////////////////////////////////////////////////////////////////////