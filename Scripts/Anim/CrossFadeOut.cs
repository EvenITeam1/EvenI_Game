using UnityEngine;
using UnityEngine.Events;

public class CrossFadeOut : MonoBehaviour {
    [Tooltip("1. 자식으로들어간 Plain & 2. 씬마다 DataTrigger 각각 다른거 넣기")]
    public UnityEvent AnimStartEvent = new UnityEvent();
    [Tooltip("1. PlayerEnable 넣기")]
    public UnityEvent AnimStopEvents = new UnityEvent();
    public AudioClip TransitionStartSound;
    private void Awake() {
        GameManager.Instance.GlobalSoundManager.MuteBGM();
        GameManager.Instance.GlobalSoundManager.MuteSFX();
    }

    public void OnAnimationStart()
    {
        AnimStartEvent.Invoke();
        GameManager.Instance.GlobalSoundManager.PlayByClip(TransitionStartSound, SOUND_TYPE.SFX);
    }
    public void OnAnimationStop()
    {
        AnimStopEvents.Invoke();
        GameManager.Instance.GlobalSoundManager.PlayByClip(TransitionStartSound, SOUND_TYPE.SFX);
        GameManager.Instance.GlobalSoundManager.UnmuteBGM();
        GameManager.Instance.GlobalSoundManager.UnmuteSFX();
    }
}