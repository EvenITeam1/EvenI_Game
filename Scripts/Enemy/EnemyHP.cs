using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour, HP
{
    [SerializeField] float _hp;
    [SerializeField] float _setHp;
    [SerializeField] Slider _hpBar;
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
        SceneManager.LoadScene("StageClearScene");
    }

    public void updateHpBar()
    {
        _hpBar.value = _hp / _setHp;
    }
}
