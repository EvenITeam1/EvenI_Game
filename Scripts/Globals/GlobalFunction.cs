using UnityEngine;

public class GlobalFunction
{
    public static bool GetIsFloatEqual(float _input, float _offset){ return -_offset <= _input && _input <= _offset;}
    public static float GetTwoValueRatio(float _max, float _currnet){ return (_currnet / _max);}
}