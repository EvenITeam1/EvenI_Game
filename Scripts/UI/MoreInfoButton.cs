using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreInfoButton : MonoBehaviour
{
    Image image;
    bool isPressed;
    [SerializeField] Color pressedColor;
    [SerializeField] GameObject moreInfoCanvas;

    private void Start()
    {
        image = GetComponent<Image>();
        isPressed = false;
    }

    public void PressButton()
    {
        if (!isPressed)
        {
            moreInfoCanvas.SetActive(true);
            isPressed = true;
            image.color = pressedColor;
        }

        else
        {
            moreInfoCanvas.SetActive(false);
            isPressed = false;
            image.color = Color.white;
        }
    }
}
