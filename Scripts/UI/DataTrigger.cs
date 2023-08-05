using UnityEngine;


/// <summary>
/// 인게임에서 사용되는 데이터 접근
/// </summary>
public class DataTrigger : MonoBehaviour
{

    //[System.Obsolete("세부로 확장되었으므로 다른 스크립트로 교체하길 바람.")]
    public void InitializeData()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.PrevHP = 100;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore = 0;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.RevivalCount = 10;
    }
    [System.Obsolete("세부로 확장되었으므로 다른 스크립트로 교체하길 바람.")]
    public void DataSave()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.PrevHP
            = RunnerManager.Instance.GlobalPlayer.playerHP.getHP();
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore
            = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
    }


    [ContextMenu("LoadData")]
    [System.Obsolete("세부로 확장되었으므로 다른 스크립트로 교체하길 바람.")]
    public void LoadData()
    {
        SaveData loadedData = GameManager.Instance.GlobalSaveNLoad.saveData;
        Player coverPlayer = GameManager.Instance.CharacterDataTableDesign.GetPlayerByINDEX(loadedData.outgameSaveData.SelectedPlayerINDEX);
        RunnerManager.Instance.GlobalPlayer.playerVisualData.spriteRenderer.sprite = coverPlayer.playerVisualData.spriteRenderer.sprite;
        RunnerManager.Instance.GlobalPlayer.playerVisualData.playerAnimator.runtimeAnimatorController = coverPlayer.playerVisualData.playerAnimator.runtimeAnimatorController;
        RunnerManager.Instance.GlobalPlayer.playerHP.setHP(GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.PrevHP);
        RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score = GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.CollectedScore;
    }

    /*********************************************************************************/

    // 러너 
    //EndMarker에 EventMarker.OnTriggerEvent()에 이벤트 바인딩이 되어 있는지 잘 확인하자.
    public void SaveRunnerGame()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.PrevHP
            = RunnerManager.Instance.GlobalPlayer.playerHP.getHP();
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore
            = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
    }

    //보스 
    //StageClear은 무조건 보스가 클리어 된 상황밖에 없다.
    public void SaveStageClear()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore
            = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;

        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.IsStageClear
            = true;
    }
    //러너  & 보스
    // MenuUI에 QuitButton에 DataTrigger.SaveGameOver()가 이벤트 바인딩 되어 있는지 잘 확인하자.
    // ReviveCanvas에 RevivalUI에 OnDieEvent()가 바인딩 잘 되어있는지 확인하자.
    public void SaveGameOver()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore
            = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.IsStageClear
            = false;
    }

    //보스
    //LevelLoader내 InvokeLevelEvent에 연결이 잘 되어있는지 확인하자
    public void LoadRunnerToBossGame()
    {
        Player coverPlayer = GameManager.Instance.CharacterDataTableDesign.GetPlayerByINDEX(GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedPlayerINDEX);
        RunnerManager.Instance.GlobalPlayer.playerVisualData.spriteRenderer.sprite = coverPlayer.playerVisualData.spriteRenderer.sprite;
        RunnerManager.Instance.GlobalPlayer.playerVisualData.playerAnimator.runtimeAnimatorController = coverPlayer.playerVisualData.playerAnimator.runtimeAnimatorController;
        RunnerManager.Instance.GlobalPlayer.playerData = coverPlayer.playerData;

        RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score = GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.CollectedScore;
        RunnerManager.Instance.GlobalPlayer.playerHP.SetMaxHP(coverPlayer.playerData.Character_hp);
        RunnerManager.Instance.GlobalPlayer.playerHP.setHP(GameManager.Instance.GlobalSaveNLoad.saveData.ingameSaveData.PrevHP);
    }

    // 러너
    // LevelLoader내 LevelLoadScript.cs 에 InvokeLevelEvent()에 이벤트 바인딩이 잘 되어있는지 확인하자.
    public void LoadOutToInGame()
    {
        Player coverPlayer = GameManager.Instance.CharacterDataTableDesign.GetPlayerByINDEX(GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedPlayerINDEX);
        RunnerManager.Instance.GlobalPlayer.playerVisualData.spriteRenderer.sprite = coverPlayer.playerVisualData.spriteRenderer.sprite;
        RunnerManager.Instance.GlobalPlayer.playerVisualData.playerAnimator.runtimeAnimatorController = coverPlayer.playerVisualData.playerAnimator.runtimeAnimatorController;
        RunnerManager.Instance.GlobalPlayer.playerData = coverPlayer.playerData;
        RunnerManager.Instance.GlobalPlayer.playerHP.SetMaxHP(coverPlayer.playerData.Character_hp);
        RunnerManager.Instance.GlobalPlayer.playerHP.setHP(coverPlayer.playerData.Character_hp);
        RunnerManager.Instance.GlobalPlayer.playerHP.recoverHp(coverPlayer.playerData.Character_per_hp_heal);
    }
    /*********************************************************************************/
}