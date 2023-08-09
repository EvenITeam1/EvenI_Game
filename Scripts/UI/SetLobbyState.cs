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

    private void Awake()
    {
        if (isBossRaidMode)
        {
            BossraidCanvas.SetActive(true);
            mainCamera.backgroundColor = bossRaidColor;
        }
           
        else
        {
            BossraidCanvas.SetActive(false);
            mainCamera.backgroundColor = normalColor;
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
        /*
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked == 10)
        {
            BossRaidModeButton.interactable = true;
            Cover.SetActive(false);
        }            

        else
        {
            BossRaidModeButton.interactable = false;
            Cover.SetActive(true);
        }     
        */

        BossRaidModeButton.interactable = true;
        Cover.SetActive(false);
    }
}
