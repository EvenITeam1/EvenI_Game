using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour, HP
{
    [SerializeField] float _hp;
    [SerializeField] float _setHp;
    
    void Start() {setHP(_setHp);}

    public void setHP(float hp) { 
        this._hp = hp; 
        if(!isAlive()){die();}
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
        _hp = 0;
        SceneManager.LoadScene("GameOverScene");
    }

}
