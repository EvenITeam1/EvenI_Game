using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRestore : MonoBehaviour
{
    void Start()
    {
        //Debug.Log(Time.timeScale);
        Time.timeScale = 1.0f;
    }
}
