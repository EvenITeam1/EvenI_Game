using UnityEngine;
using UnityEngine.Events;

public class CrossFadeOut : MonoBehaviour {
    private Player player;
    private UnityEvent AnimStartEvent = new UnityEvent();
    private UnityEvent AnimStopEvents = new UnityEvent();
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player == null) {throw new System.Exception("플레이어 태그 없음");}
        if(transform.GetChild(0).name != "Plain") {throw new System.Exception("자식 오브젝트가 잘못 연결됨");}
        
        AnimStartEvent.RemoveAllListeners();
        AnimStartEvent.AddListener(() => transform.GetChild(0).gameObject.SetActive(true));
        AnimStartEvent.AddListener(() => GameObject.Find("DataTrigger").GetComponent<DataTrigger>().LoadData());
        
        AnimStopEvents.RemoveAllListeners();
        AnimStopEvents.AddListener(() => player.PlayerEnable());
    }

    public void OnAnimationStart(){
        AnimStartEvent.Invoke();
    }
    public void OnAnimationStop(){
        AnimStopEvents.Invoke();
    }
}