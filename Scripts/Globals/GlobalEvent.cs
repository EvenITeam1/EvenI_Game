using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Ref, Out에 대한 이해가 필요하다
/// > 함수 매개변수를 Call by Reference 하기 위해서 사용한다. <br/>
/// > Call By Value매개변수는 함수값 VS 함수 호출자 위치값 별개의 값이 된다. <br/>
/// > 따라서 함수에서 변경된값을 외부 호출자 위치에도 적용 시키려면 Ref라는 문법을 써야한다  <br/>
/// </summary>
/// <param name="input"></param>
/// <typeparam name="T"></typeparam>

public enum EVENT_FLAG {
    BOOL, FLOAT, STRING
}


public class GlobalEvent : MonoBehaviour
{
    private void Awake()
    {
        scoreCheck ??= GetComponent<ScoreCheck>();
    }
    void Update()
    {
        Time.timeScale = GameTimeScale;
    }
    
    /////////////////////////////////////////////////////////////////////////////////
#region ScoreCheck
    public ScoreCheck scoreCheck;
#endregion
    /////////////////////////////////////////////////////////////////////////////////
#region TimeScaleEventHandler

    public UnityEvent       PausedEvent;
    [Range(0, 1)]
    public float            GameTimeScale = 1f;
    float                   mCurrentTimeScale = 1f;
    bool                    mIsGamePaused = false;
    bool                    mIsSlowed = false;

    public bool IsGamePaused
    {
        get
        {
            return mIsGamePaused;
        }
        set
        {
            if (value == true) { GameTimeScale = 0; }
            else { GameTimeScale = mCurrentTimeScale; }
            mIsGamePaused = value;
           //Debug.Log("Time Changed");
        }
    }

    public void HandleTimeSlow()
    {
        if (mIsSlowed) return;
    }

    //DotTween 사용해서 증가 커브 설정하기
#endregion
    /////////////////////////////////////////////////////////////////////////////////
#region DebugEvent
    public void PrintString(string _input){
       //Debug.Log(_input);
    }
#endregion
}