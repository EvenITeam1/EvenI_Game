using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobHP : MonoBehaviour, HP
{
    [SerializeField] float _hp;
    [SerializeField] float _setHp;

    float _time;

    void Start() {
        setHP(_setHp);
        _time = 0;
    }

    public void setHP(float hp)
    {
       _hp = hp; 
        if (!isAlive()) {
            die();
        }
    }

    public float getHP() { return this._hp; }
    public float getMaxHp() {return this._setHp;}
    public bool isAlive()
    {
        if (_hp > 0)
            return true;
        else
            return false;
    }

    public void die()
    {
        _hp = 0; gameObject.SetActive(false);
    }
}
