using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLobbyState : MonoBehaviour
{
    private static bool isBossRaidMode = false;
    [SerializeField] GameObject BossraidCanvas;
    [SerializeField] Camera mainCamera;
    [SerializeField] Color normalColor;
    [SerializeField] Color bossRaidColor;
    [SerializeField] Button BossRaidModeButton;
    [SerializeField] GameObject Cover;
    [SerializeField] GameObject background;

    private void Awake()
    {
        if (isBossRaidMode)
        {
            BossraidCanvas.SetActive(true);
            mainCamera.backgroundColor = bossRaidColor;
            background.SetActive(false);
        }
           
        else
        {
            BossraidCanvas.SetActive(false);
            mainCamera.backgroundColor = normalColor;
            background.SetActive(true);
        }          
    }
    private void Start()
    {
        SetBossModeButton();
    }

    public void SetLobbyNormalState()
    {
        isBossRaidMode = false;
    }

    public void SetLobbyBossRaidState()
    {
        isBossRaidMode = true;
    }

    void SetBossModeButton()
    {
        
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked == 6)//it will be updated by 10
        {
            BossRaidModeButton.interactable = true;
            Cover.SetActive(false);
        }            

        else
        {
            BossRaidModeButton.interactable = false;
            Cover.SetActive(true);
        }     
    }
}
