using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class BossHP : MonoBehaviour
{
    [SerializeField] float _hp;
    [SerializeField] float _setHp;
    [SerializeField] Slider _hpBar;
    [SerializeField] int bossScore;
    public bool isBossRaid;
    public int stageIndex;
    [SerializeField] List<GameObject> bossGroups;
    [SerializeField] List<GameObject> bossPatternManagerList;
    [SerializeField] GameObject CountDownObj;
    private SpriteRenderer spriteRenderer;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        setHP(_setHp);
        updateHpBar();
    }

    public void setHP(float hp)
    {
        _hp = hp;

        if (!isAlive())
            die().Forget();
    }
    public float getMaxHp() { return this._setHp; }
    public float getHP() { return this._hp; }

    public bool isAlive()
    {
        if (_hp > 0)
            return true;
        else
            return false;
    }

    async UniTaskVoid die()
    {
        if (isBossRaid)
        {
            _hp = 0;
            if(stageIndex == 5)
                RunnerManager.Instance.GlobalEventInstance.BroadCastBossDie();
            else
            {
                CountDownObj.SetActive(true);
                bossPatternManagerList[stageIndex].SetActive(false);
                bossGroups[stageIndex].SetActive(false);
                RunnerManager.Instance.GlobalPlayer.HealAbs(50);
                await UniTask.Delay(TimeSpan.FromSeconds(4));
                bossGroups[stageIndex + 1].SetActive(true);
            }      
        }

        else
        {
            _hp = 0;
            RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score += bossScore;
            gameObject.SetActive(false);
            RunnerManager.Instance.GlobalEventInstance.BroadCastBossDie();
        }      
    }

    public void updateHpBar() 
    {
        _hpBar.value = _hp / _setHp;
    }
    public void GetDamaged(){
        StartCoroutine(AsyncOnHitVisual());
        GameManager.Instance.GlobalSoundManager.PlaySFXByString("SFX_GetHit_1");
    }

    IEnumerator AsyncOnHitVisual()
    {
        spriteRenderer.color = Color.red;
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}