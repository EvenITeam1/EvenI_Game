using UnityEngine;

[System.Serializable]
public class SaveData{
    [SerializeField]
    public int RevivalCount;
    [SerializeField]    
    public float prevHP;
    public float prevScore;
    public SaveData(){
        RevivalCount = -1;
        prevHP = -1;
        prevScore = 0;
    }
    public PlayerData layerData;
}

public class SaveNLoadManager : MonoBehaviour {
    [SerializeField]
    public SaveData saveData;

    public ref SaveData GetSaveDataByRef(){
        return ref this.saveData;
    }
}