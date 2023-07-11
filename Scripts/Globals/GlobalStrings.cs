using UnityEngine;

public enum PROJECT_LAYERS {
Default = 0, 
TransparentFX = 1, Ignore_RayCast, Ground, Water, UI, BackGround, Ghost, Trap, Item, Player, 
Zone = 11, NONE_12, NONE_13, NONE_14, NONE_15,NONE_16,NONE_17,NONE_18,NONE_19,NONE_20,
NONE_21, NONE_22, NONE_23, NONE_24, SlowDown = 25, EnemyCenter, Flag, Mirror, Bullet, Enemy
}

public static class GlobalStrings {
    public static readonly string[] LAYERS_STRING = {
        "Default", 
        "TransparentFX", "Ignore RayCast", "Ground", "Water", "UI" , "BackGround", "Ghost", "Trap", "Item", "Player", 
        "Zone", "NONE_12", "NONE_13", "NONE_14", "NONE_15", "NONE_16", "NONE_17", "NONE_18", "NONE_19", "NONE_20", 
        "NONE_21", "NONE_22", "NONE_23", "NONE_24", "SlowDown", "EnemyCenter", "Flag", "Mirror", "Bullet", "Enemy"
    };
}