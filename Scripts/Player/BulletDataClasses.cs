using UnityEngine;

[System.Serializable]
public class BulletData {
    public BULLET_INDEX Index;
    public string   Bullet_name;
    public BULLET_CATEGORY Bullet_category;
    public float  Bullet_width;
    public float  Bullet_height;
    public float  Bullet_min_dmg;
    public float  Bullet_max_dmg;
    public float  Bullet_set_dmg;
    public float  Bullet_crit_chance;
    public float  Bullet_crit_const;
    public float  Bullet_speed;
    public float  Bullet_time;
    public int    Bullet_reflect_count;
    public string Bullet_filecode;
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