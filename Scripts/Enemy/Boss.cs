using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private BOSS_INDEX Index;

    [SerializeField]
    public BossData bossData;

    [SerializeField]
    public BossHP bossHP;

    private void Start()
    {
        bossHP.setHP(bossData.Boss_hp);
    }

    public void InitializeAfterAsynchronous(){
        bossData = GameManager.Instance.BossDataTableDesign.GetBossDataByINDEX(this.Index);
    }
}
