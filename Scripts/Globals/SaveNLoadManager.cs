using UnityEngine;

public class SaveNLoadManager : MonoBehaviour {
    [System.Serializable]
    public class SaveData{
        [SerializeField]
        public OutGameData outGameData;

        [SerializeField]
        public PlayerData playerData;

        public SaveData() {
            outGameData = new OutGameData();
            playerData = GameManager.Instance.GlobalPlayer.playerData;
        }
    }

    [SerializeField]
    public SaveData saveData;

    private void Awake() {
        this.saveData = new SaveData();
    }
}