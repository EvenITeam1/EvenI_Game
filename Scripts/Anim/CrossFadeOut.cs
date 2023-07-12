using UnityEngine;
using UnityEngine.Events;

public class CrossFadeOut : MonoBehaviour {
    public UnityEvent AnimStartEvent;
    public UnityEvent AnimStopEvents;
    public void OnAnimationStart(){
        AnimStartEvent.Invoke();
    }
    public void OnAnimationStop(){
        AnimStopEvents.Invoke();
    }
}