using UnityEngine;

namespace TwoDimensions
{
    public class GlobalFunction : MonoBehaviour
    {
        public static bool GetIsFloatEqual(float _input, float _offset){ return -_offset <= _input && _input <= _offset;}
    }
}