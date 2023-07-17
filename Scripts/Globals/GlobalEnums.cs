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

#endregion 

/////////////////////////////////////////////////////////////////////////////////

#region Bullets
    public enum BULLET_INDEX {
        DEFAULT = 3000,
        B_3001, B_3002, B_3003, B_3004
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
        M_4001, M_4002 ,M_4003 ,M_4004 ,M_4005 , M_4006
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

/////////////////////////////////////////////////////////////////////////////////
