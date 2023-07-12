using UnityEngine;

public class SaveNLoadManager : MonoBehaviour {
    [System.Serializable]
    public class SaveData{
        [SerializeField]
        public OutGameData outGameData;

        [SerializeField]
        public PlayerData playerData;
    }

    [SerializeField]
    public SaveData saveData;
}