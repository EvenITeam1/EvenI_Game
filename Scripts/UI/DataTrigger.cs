using UnityEngine;

public class DataTrigger : MonoBehaviour {
    
    public void InitializeData() {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().prevHP          = 100;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().prevScore       = 0;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().RevivalCount    = 0;
    }
    public void DataSave(){
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().prevHP    = RunnerManager.Instance.GlobalPlayer.playerHP.getHP();
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().prevScore = RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
    }

    public void LoadData(){
        RunnerManager.Instance.GlobalPlayer.playerHP.setHP(GameManager.Instance.GlobalSaveNLoad.saveData.prevHP);
        RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score = GameManager.Instance.GlobalSaveNLoad.saveData.prevScore;
    }
}