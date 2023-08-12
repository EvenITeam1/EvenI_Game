using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLockForce : MonoBehaviour
{
    [SerializeField] Button button;
    //s Update is called once per frame
    void Update()
    {
        button.interactable = false;
    }
}
