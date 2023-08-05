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
        if (goldAmount != null) { goldAmount.text = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedCoin.ToString(); }
        if (expAmount != null) { expAmount.text = "EXP " + GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.CollectedExp.ToString() + "/1000"; }    
    }
}
