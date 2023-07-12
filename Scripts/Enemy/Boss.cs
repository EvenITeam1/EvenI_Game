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

    private async void Awake()
    {
        bossData = await GameManager.Instance.BossDataTableDesign.GetBossDataByINDEX(this.Index); //외부에서 받는것
        bossHP.setHP(bossData.Boss_hp);
    }
}
