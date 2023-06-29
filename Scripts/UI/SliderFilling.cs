using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderFilling : MonoBehaviour {
    public Slider slider;
    public DestinationAchivementChecker destinationAchivementChecker;
    private void Awake() {
        destinationAchivementChecker ??= GameObject.Find("Map").GetComponent<DestinationAchivementChecker>();
    }
    private void Update() {
        slider.value = destinationAchivementChecker.GetRatioOfDistance();
    }
}