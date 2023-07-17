using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TwoDimensions
{
    public class SliderFilling : MonoBehaviour
    {
        public Slider slider;
        public DestinationAchivementChecker destinationAchivementChecker;
        
        private void Start() {
            destinationAchivementChecker = GameObject.Find("Map").GetComponent<DestinationAchivementChecker>();
        }
        private void Update()
        {
            slider.value = destinationAchivementChecker.GetRatioOfDistance();
        }
    }
}
