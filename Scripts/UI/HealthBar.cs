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
        public PlayerHP playerHP;
        public Slider slider;
        public Image fill;
        public Gradient gradient;

        public TextMeshProUGUI text;

        private void Awake()
        {
            fill.color = gradient.Evaluate(1f);
        }
        private void Start() {
            playerHP = RunnerManager.Instance.GlobalPlayer.playerHP;
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
