using UnityEngine;
using UnityEngine.Events;

public class CrossFadeOut : MonoBehaviour {
    public UnityEvent AnimStopEvents;
    public void OnAnimationStop(){
        AnimStopEvents.Invoke();
    }
}