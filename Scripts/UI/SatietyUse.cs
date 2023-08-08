using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Text;

public class SatietyUse : MonoBehaviour
{
    [SerializeField] GameObject EnterFailedCanvas;
    [SerializeField] GameObject EnterCanvas;
    [SerializeField] List<GameObject> EnterMessageCanvas;

    #region stageEnterFunction
    public void Stage1ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true); 
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[0].SetActive(true);
            ResultHandler.SendEnterStageData(0);
        }        
    }
    public void Stage2ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[1].SetActive(true);
            ResultHandler.SendEnterStageData(1);
        }
    }

    public void Stage3ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[2].SetActive(true);
            ResultHandler.SendEnterStageData(2);
        }
    }

    public void Stage4ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[3].SetActive(true);
            ResultHandler.SendEnterStageData(3);
        }
    }

    public void Stage5ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[4].SetActive(true);
            ResultHandler.SendEnterStageData(4);
        }
    }

    public void Stage6ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[5].SetActive(true);
            ResultHandler.SendEnterStageData(5);
        }
    }

    public void Stage7ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[6].SetActive(true);
            ResultHandler.SendEnterStageData(6);
        }
    }

    public void Stage8ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[7].SetActive(true);
            ResultHandler.SendEnterStageData(7);
        }
    }

    public void Stage9ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[8].SetActive(true);
            ResultHandler.SendEnterStageData(8);
        }
    }

    public void Stage10ChargeCountCheckInStoryMode()
    {
        if (SatietyManage.GetChargeCount() < 1)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
            EnterMessageCanvas[9].SetActive(true);
            ResultHandler.SendEnterStageData(9);
        }
    }

    public void ChargeCountCheckInBossMode()
    {
        if (SatietyManage.GetChargeCount() < 3)
        {
            EnterFailedCanvas.SetActive(true);
        }

        else
        {
            EnterCanvas.SetActive(true);
        }
    }

    #endregion

    public void UseChargeCount(int n)
    {
        SatietyJsonData satieTyJsonData = new SatietyJsonData(DateTime.Now, SatietyManage.GetChargeCount() - n, SatietyManage.GetPassedTimeInLobby());
        var result = JsonConvert.SerializeObject(satieTyJsonData);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.persistentDataPath, "QuitTimeData"), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(result);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
   public void UseSatietyInStoryMode()
   {   
        UseChargeCount(1);
   }

    public void UseSatietyInBossMode()
    {
        UseChargeCount(3);
    }
}
