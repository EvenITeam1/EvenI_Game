using UnityEngine;
using UnityEngine.Events;

public class CrossFadeOut : MonoBehaviour {
    [Tooltip("1. 자식으로들어간 Plain & 2. 씬마다 DataTrigger 각각 다른거 넣기")]
    public UnityEvent AnimStartEvent;
    [Tooltip("1. PlayerEnable 넣기")]
    public UnityEvent AnimStopEvent;
    public AudioClip TransitionStartSound;
    private void Awake() {
        GameManager.Instance.GlobalSoundManager.MuteBGM();
        GameManager.Instance.GlobalSoundManager.MuteSFX();
        AnimStartEvent = new UnityEvent();
        AnimStopEvent = new UnityEvent();
        AnimStartEvent.AddListener(() => {transform.GetChild(0).gameObject.SetActive(true);});
        AnimStopEvent.AddListener(()=> {RunnerManager.Instance.GlobalPlayer.PlayerEnable();});
    }
    
    public void OnAnimationStart()
    {
        AnimStartEvent.Invoke();
        GameManager.Instance.GlobalSoundManager.PlayByClip(TransitionStartSound, SOUND_TYPE.SFX);
    }
    public void OnAnimationStop()
    {
        AnimStopEvent.Invoke();
        GameManager.Instance.GlobalSoundManager.PlayByClip(TransitionStartSound, SOUND_TYPE.SFX);
    }
}