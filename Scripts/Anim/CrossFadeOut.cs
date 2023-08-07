using UnityEngine;
using UnityEngine.Events;

public class CrossFadeOut : MonoBehaviour {
    [Tooltip("1. 자식으로들어간 Plain & 2. 씬마다 DataTrigger 각각 다른거 넣기")]
    public UnityEvent AnimStartEvent = new UnityEvent();
    [Tooltip("1. PlayerEnable 넣기")]
    public UnityEvent AnimStopEvents = new UnityEvent();
    public AudioClip TransitionStartSound;
    private void Awake() {
        //if(transform.GetChild(0).name != "Plain") {throw new System.Exception("자식 오브젝트가 잘못 연결됨");}
        
        //AnimStartEvent.RemoveAllListeners();
        //AnimStartEvent.AddListener(() => transform.GetChild(0).gameObject.SetActive(true));
        //AnimStartEvent.AddListener(() => GameObject.Find("DataTrigger").GetComponent<DataTrigger>().LoadData());

        //AnimStopEvents.RemoveAllListeners();
        //AnimStopEvents.AddListener(() => player.PlayerEnable());
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
    }
}