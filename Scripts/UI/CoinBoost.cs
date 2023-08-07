using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBoost : MonoBehaviour
{
    [SerializeField] Button BosstOnButton;
    [SerializeField] GameObject BoostWarningCanvas;
    private void Start()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.isCoinBoostOn = false;
        BosstOnButton.interactable = true;
    }

    public void PressCoinBoostOnButton()
    {
        if (GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.Coin15Count <= 0)
            BoostWarningCanvas.SetActive(true);

        else
        {
            GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.isCoinBoostOn = true;
            BosstOnButton.interactable = false;
        }      
    }
}
