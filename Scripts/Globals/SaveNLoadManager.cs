using UnityEngine;

[System.Serializable]
public struct SaveData{
    [SerializeField]
    public int RevivalCount;
    
    [SerializeField]
    public float prevHP;
    public float prevScore;
}

public class SaveNLoadManager : MonoBehaviour {
    [SerializeField]
    public SaveData saveData;

    public ref SaveData GetSaveDataByRef(){
        return ref this.saveData;
    }
}