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
    public GlobalEvent      GlobalEventInstance;
}