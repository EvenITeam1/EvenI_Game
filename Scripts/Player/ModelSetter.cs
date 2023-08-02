using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSetter : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController newController;

    [ContextMenu("Set Animator Controller")]
    public void SetAnimator()
    {
        animator.runtimeAnimatorController = newController;
    }
}
