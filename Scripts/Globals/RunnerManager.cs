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
    
    [HideInInspector] public Player           GlobalPlayer;
    [HideInInspector] public MobGenerator     GlobalMobGenerator;
    [HideInInspector] public GameObject       GlobalMap;
    [HideInInspector] public GlobalEvent      GlobalEventInstance;

    private void Awake() {
        GlobalPlayer        = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GlobalMobGenerator  = GameObject.FindGameObjectWithTag("MobGenerator").GetComponent<MobGenerator>();
        GlobalMap           = GameObject.FindGameObjectWithTag("Map");
        GlobalEventInstance = transform.GetChild(0).GetComponent<GlobalEvent>();
    }
}