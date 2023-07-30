using UnityEngine;

public class DataTrigger : MonoBehaviour {
    
    public void InitializeData(){
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().prevHP = 100;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().prevScore = 0;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().RevivalCount = 10;
    }
    public void DataSave(){
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().prevHP    = RunnerManager.Instance.GlobalPlayer.playerHP.getHP();
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().prevScore = RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
    }

    [ContextMenu("LoadData")]
    public void LoadData(){
        SaveData loadedData = GameManager.Instance.GlobalSaveNLoad.saveData;
        Player coverPlayer = GameManager.Instance.CharacterDataTableDesign.GetPlayerByINDEX(loadedData.receivedPlayerData.Index);
        RunnerManager.Instance.GlobalPlayer.playerVisualData.spriteRenderer.sprite = coverPlayer.playerVisualData.spriteRenderer.sprite;
        RunnerManager.Instance.GlobalPlayer.playerVisualData.playerAnimator.runtimeAnimatorController = coverPlayer.playerVisualData.playerAnimator.runtimeAnimatorController;
        RunnerManager.Instance.GlobalPlayer.playerHP.setHP(GameManager.Instance.GlobalSaveNLoad.saveData.prevHP);
        RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score = GameManager.Instance.GlobalSaveNLoad.saveData.prevScore;
    }
}