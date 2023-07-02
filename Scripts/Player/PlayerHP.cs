using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour, HP
{
    [SerializeField] float _hp;
    [SerializeField] float _setHp;
    [SerializeField] float _recoverHp;
    [SerializeField] float _recoverInterval;
    float _time;

    void Start() {
        setHP(_setHp);
        _time = 0;
    }

    private void Update()
    {
        if(_time < _recoverInterval)
        {
            _time += Time.deltaTime;
        }

        else
        {
            recoverHp(_recoverHp);
            _time = 0;
        }
    }

    public void setHP(float hp)
    {
       _hp = hp; 
        if (!isAlive())
        {
            die();
        }
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
        SceneManager.LoadScene("gameOverScene");
    }

    public void recoverHp(float recoverHp)//혹시나 회복 템 나오면 이 함수 쓰라고 매개변수 달아놓음
    {
        if(_hp < _setHp)
        {
            _hp += recoverHp;
        }     
    }

}
