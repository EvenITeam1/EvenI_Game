using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
internal static class YieldInstructionCache
{
    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals (float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode (float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> _timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        seconds = (float)Math.Round(seconds, 3);
        WaitForSeconds wfs;
        if (!_timeInterval.TryGetValue(seconds, out wfs))
            _timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }
}

//https://coderzero.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EB%A7%A4%EB%89%B4%EC%96%BC-%EC%BD%94%EB%A3%A8%ED%8B%B4CoRoutine
//https://ejonghyuck.github.io/blog/2016-12-12/unity-coroutine-optimization/