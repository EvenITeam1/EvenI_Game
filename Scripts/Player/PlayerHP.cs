using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour, HP
{
    [SerializeField] float _hp;
    [SerializeField] float _setHp;
    [SerializeField] public float _recoverHp;
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
        _hp = 0;
        SceneManager.LoadScene("gameOverScene");
    }

    public void recoverHp(float recoverHp)//Ȥ�ó� ȸ�� �� ������ �� �Լ� ����� �Ű����� �޾Ƴ���
    {
        if(_hp < _setHp)
        {
            _hp += recoverHp;
        }     
    }

}
