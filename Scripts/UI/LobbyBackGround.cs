using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBackGround : MonoBehaviour
{
    [SerializeField] List<GameObject> backgrounds;
    int currentHIghestUnlockLevel;
    void Start()
    {
        currentHIghestUnlockLevel = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.HightestStageUnlocked;
        if (currentHIghestUnlockLevel >= 6)
            currentHIghestUnlockLevel = 5;
        ActiveBackground();
    }

    void OffAllBackgrounds()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].SetActive(false);
        }
    }
    void ActiveBackground()
    {
        OffAllBackgrounds();
        if (currentHIghestUnlockLevel == 0)
            return;

        else
        {
            int n = Random.Range(0, currentHIghestUnlockLevel);
            backgrounds[n].SetActive(true);
        }
    }
  
}
