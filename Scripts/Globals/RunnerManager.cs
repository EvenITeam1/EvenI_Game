using UnityEngine;

public class RunnerManager : MonoBehaviour {
    private static RunnerManager _instance;
    public static RunnerManager Instance
    {
        get {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(RunnerManager)) as RunnerManager;

                if (_instance == null) Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    
    public Player           GlobalPlayer;
    public MobGenerator     GlobalMobGenerator;
    public GameObject       GlobalMap;
    public GlobalEvent      GlobalEventInstance;


    [ContextMenu("Load RunnerManager Elements")]
    public void LRM(){
        GlobalPlayer        = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GlobalMobGenerator  = GameObject.FindGameObjectWithTag("MobGenerator").GetComponent<MobGenerator>();
        GlobalMap           = GameObject.FindGameObjectWithTag("Map");
        GlobalEventInstance = transform.GetChild(0).GetComponent<GlobalEvent>();
    }

    private void Awake() {
        GlobalPlayer        ??= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GlobalMobGenerator  ??= GameObject.FindGameObjectWithTag("MobGenerator").GetComponent<MobGenerator>();
        GlobalMap           ??= GameObject.FindGameObjectWithTag("Map");
        GlobalEventInstance ??= transform.GetChild(0).GetComponent<GlobalEvent>();
    }
}