using UnityEngine;


/// <summary>
/// 인게임에서 사용되는 데이터 접근
/// </summary>
public class DataTrigger : MonoBehaviour {
    
    [System.Obsolete("세부로 확장되었으므로 다른 스크립트로 교체하길 바람.")]
    public void InitializeData(){
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.PrevHP = 100;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore = 0;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.RevivalCount = 10;
    }
    [System.Obsolete("세부로 확장되었으므로 다른 스크립트로 교체하길 바람.")]
    public void DataSave(){
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.PrevHP    
            = RunnerManager.Instance.GlobalPlayer.playerHP.getHP();
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore 
            = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
    }

    
    [System.Obsolete("세부로 확장되었으므로 다른 스크립트로 교체하길 바람.")]
    [ContextMenu("LoadData")]
    public void LoadData(){
        SaveData loadedData = GameManager.Instance.GlobalSaveNLoad.saveData;
        Player coverPlayer = GameManager.Instance.CharacterDataTableDesign.GetPlayerByINDEX(loadedData.outgameSaveData.SelectedPlayerData.Index);
        RunnerManager.Instance.GlobalPlayer.playerVisualData.spriteRenderer.sprite = coverPlayer.playerVisualData.spriteRenderer.sprite;
        RunnerManager.Instance.GlobalPlayer.playerVisualData.playerAnimator.runtimeAnimatorController = coverPlayer.playerVisualData.playerAnimator.runtimeAnimatorController;
        RunnerManager.Instance.GlobalPlayer.playerHP.setHP(GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.PrevHP);
        RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score = GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.CollectedScore;
    }
    
    /*********************************************************************************/

    public void SaveRunnerGame(){
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.PrevHP    
            = RunnerManager.Instance.GlobalPlayer.playerHP.getHP();
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore 
            = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
    }

    //StageClear은 무조건 보스가 클리어 된 상황밖에 없다.
    public void SaveStageClear(){
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore   
            = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.IsStageClear
            = true;
    }

    public void SaveGameOver(){
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore   
            = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.IsStageClear
            = false;
    }

    public void LoadRunnerToBossGame(){
        RunnerManager.Instance.GlobalPlayer.playerHP.setHP(GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.PrevHP);
        RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score 
            = GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.CollectedScore;
    }
    public void LoadOutToInGame(){
        Player coverPlayer = GameManager.Instance.CharacterDataTableDesign.GetPlayerByINDEX(GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedPlayerData.Index);
        RunnerManager.Instance.GlobalPlayer.playerVisualData.spriteRenderer.sprite = coverPlayer.playerVisualData.spriteRenderer.sprite;
        RunnerManager.Instance.GlobalPlayer.playerVisualData.playerAnimator.runtimeAnimatorController = coverPlayer.playerVisualData.playerAnimator.runtimeAnimatorController;
        RunnerManager.Instance.GlobalPlayer.playerData = coverPlayer.playerData;
    }
    /*********************************************************************************/
}