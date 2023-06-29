using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;


namespace TwoDimensions
{
    public class HealthBar : MonoBehaviour
    {
        public TwoDimensions.PlayerHP_Two playerHP;
        public Slider slider;
        public Image fill;
        public Gradient gradient;

        public Text text;

        private void Awake()
        {
            fill.color = gradient.Evaluate(1f);
            playerHP ??= GameObject.Find("Player").GetComponent<TwoDimensions.PlayerHP_Two>();
        }

        private void Update()
        {
            SetSlider();
        }

        public void SetSlider()
        {
            slider.value =  GlobalFunction.GetTwoValueRatio(playerHP.getMaxHp(), playerHP.getHP()) * slider.maxValue;
            text.text = $"{(int)playerHP.getHP()} / {(int)playerHP.getMaxHp()}";
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
