using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatietyUse : MonoBehaviour
{
   public void UseSatietyInStoryMode()
   {
        SatietyManage.UseChargeCount(1);
   }

    public void UseSatietyInBossMode()
    {
        SatietyManage.UseChargeCount(3);
    }
}
