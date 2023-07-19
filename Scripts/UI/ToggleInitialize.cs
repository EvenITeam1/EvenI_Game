using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInitialize : MonoBehaviour
{
    [SerializeField] Image background1;
    [SerializeField] Color seletedColor;
    private void Start()
    {
        background1.color = seletedColor;
    }

    public void resetColor()
    {
        background1.color = Color.white;
    }
}
