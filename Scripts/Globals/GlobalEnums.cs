#region Players

    public enum DOG_INDEX {
        DEFAULT = 1000,
        C_01, C_02, C_03, C_04, C_05, C_06, C_07, C_08, C_09, C_10
    }

#endregion 

/////////////////////////////////////////////////////////////////////////////////

#region Objects

    public enum OBJECT_INDEX {
        DEFAULT = 2000, 
        OB_2001, OB_2002, OB_2003, OB_2004, OB_2005, OB_2006, OB_2007, OB_2008, OB_2009, 
        OB_2010, OB_2011, OB_2012, OB_2013, OB_2014, OB_2015, OB_2016, OB_2017, OB_2018, OB_2019, 
        OB_2020
    }

    public enum OBJECT_CATEGORY {
        DEFAULT = 0, TRAP, COIN, PLATFORM, HOLE, ITEM
    }

    public enum OBJECT_ITEM_CATEGORY {
        DEFAULT = 0, SMALL_HEAL, MID_HEAL, BARRIER
    }

#endregion 

/////////////////////////////////////////////////////////////////////////////////

#region Bullets
    public enum BULLET_INDEX {
        B_TEST = 2999,
        DEFAULT = 3000,
        B_3001, B_3002, B_3003, B_3004, B_3005, B_3006, B_3007, B_3008, B_3009, B_3010,
        B_3011, B_3012, B_3013, B_3014, B_3015, B_3016, B_3017, B_3018, B_3019, B_3020,
        B_3021, B_3022, B_3023, B_3024, B_3025, B_3026, B_3027, B_3028, B_3029, B_3030,
        B_3031, B_3032, B_3033, B_3034
    }

    public enum BULLET_CATEGORY {
        DEFAULT = 0, NORMAL, REFLECT 
    }

    public enum LASER_INDEX {
        DEFAULT = 3100,
        L_3101, L_3102
    }

    public enum LASER_CATEGORY {
        DEFAULT = 0, STATIC, DYNAMIC
    }

#endregion

/////////////////////////////////////////////////////////////////////////////////

#region Mobs

    public enum MOB_INDEX {
        DEFAULT = 4000,
        M_4001, M_4002, M_4003, M_4004, M_4005, M_4006, M_4007, M_4008, M_4009, M_4010,
        M_4011, M_4012, M_4013, M_4014, M_4015, M_4016, M_4017, M_4018, M_4019, M_4020, 
        M_4021, M_4022, M_4023, M_4024, M_4025, M_4026, M_4027, M_4028, M_4029, M_4030, 
        M_4031
    }

#endregion 

/////////////////////////////////////////////////////////////////////////////////

#region Boss

public enum BOSS_INDEX {
    DEFAULT = 5000,
    BO_5001
}

public enum BOSS_CATEGORY {
    DEFAULT = 0, NORMAL
}

#endregion 

/////////////////////////////////////////////////////////////////////////////////

#region  Movements 
public enum MOVEMENT_INDEX {
        HOLD = 0, DOWN, UP, LEFT, RIGHT, VERTICAL_LOOP, HORIZONTAL_LOOP
    }

#endregion 

#region Tips

public enum TIP_INDEX {
    DEFAULT = 9000,
    TIP_9001, TIP_9002, TIP_9003, TIP_9004, TIP_9005, TIP_9006, TIP_9007, TIP_9008, TIP_9009, TIP_9010, TIP_9011, TIP_9012, TIP_9013
}
#endregion

/////////////////////////////////////////////////////////////////////////////////