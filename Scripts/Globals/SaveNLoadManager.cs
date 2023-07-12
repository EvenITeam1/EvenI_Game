using UnityEngine;

public class SaveNLoadManager : MonoBehaviour {
    [System.Serializable]
    public class SaveData{
        [SerializeField]
        public OutGameData outGameData;

        [SerializeField]
        public PlayerHP playerHp;
    }

    [SerializeField]
    public SaveData saveData;

    public void Init(){
        saveData.playerHp = RunnerManager.Instance.GlobalPlayer.playerHP;
    }

    public void DataSave(){
        this.saveData.playerHp = RunnerManager.Instance.GlobalPlayer.playerHP;
    }
    public SaveData DataLoad(){
        return this.saveData;
    }
}