using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHP : MonoBehaviour, HP
{
    [SerializeField] float _hp;
    [SerializeField] float _setHp;
    [SerializeField] Slider _hpBar;

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
            die();
    }
    public float getMaxHp(){return this._setHp;}
    public float getHP() { return this._hp; }

    public bool isAlive()
    {
        if (_hp > 0)
            return true;
        else
            return false;
    }
    
    public void die()
    {
        _hp = 0;
        gameObject.SetActive(false);
        RunnerManager.Instance.GlobalEventInstance.BroadCastBossDie();
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
