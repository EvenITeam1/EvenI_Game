using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutGameDataManage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldAmount;
    [SerializeField] TextMeshProUGUI expAmount;

    private void Awake()
    {

        goldAmount.text = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin.ToString();
        expAmount.text = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp.ToString();
    }
}
