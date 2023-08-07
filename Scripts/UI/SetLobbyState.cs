using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLobbyState : MonoBehaviour
{
    private static bool isBossRaidMode = false;
    [SerializeField] GameObject BossraidCanvas;
    [SerializeField] Camera mainCamera;
    [SerializeField] Color normalColor;
    [SerializeField] Color bossRaidColor;

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

    public void SetLobbyNormalState()
    {
        isBossRaidMode = false;
    }

    public void SetLobbyBossRaidState()
    {
        isBossRaidMode = true;
    }

}
