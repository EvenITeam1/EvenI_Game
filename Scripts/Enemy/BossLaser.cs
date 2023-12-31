using System.Collections;
using System.Collections.Generic;
using TwoDimensions;
using Unity.VisualScripting;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    [SerializeField] bool _isStatic;
    [SerializeField] LayerMask _playerLayer;
    private int _damage;
    [SerializeField] int _setDmg;
    [SerializeField] float _width;
    [SerializeField] float _warningLastTime;
    [SerializeField] GameObject _warningEffect;
    [SerializeField] GameObject _laserEffect;
    float _time;


    [SerializeField] bool _targetIsPlayer;
    [SerializeField] float _shotLastTime;
    [SerializeField] Vector2 _shotPoint;


    [SerializeField] Vector2 _startPoint;
    [SerializeField] Vector2 _endPoint;
    [SerializeField] float _moveSpeed;
    bool _ready;

    private void Awake()
    {
        setDamage(_setDmg);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _time = 0;
        _ready = false;
    }
    private void FixedUpdate()
    {
        switch(_isStatic)
        {
            case true: laserStatic(_width, _shotPoint);
                break;

            case false: laserDynamic(_width, _startPoint, _endPoint, _moveSpeed);
                break;
        }
    }

    private void laserShot(float width)
    {
        Vector2 objPos = transform.position;
        Vector2 leftUp = objPos + new Vector2(-100, width /2);
        Vector2 rightDown = objPos + new Vector2(0, -width /2);
        var targets = Physics2D.OverlapAreaAll(leftUp, rightDown, _playerLayer);
        var targetN = targets.Length;

        for(int i = 0; i<targetN; i++)
        {
            if (targets[i].gameObject.GetComponent<PlayerHP>())
            {
                targets[i].GetComponent<Player>().GetDamage(_damage);
            }
        } 
    }

    public void laserStatic(float width, Vector2 shotPoint)
    {
        if (transform.position != (Vector3)shotPoint)
            transform.position = shotPoint;

        if (_time < _shotLastTime + _warningLastTime)
        {
            _time += Time.deltaTime;

            if (_time < _warningLastTime && !_warningEffect.activeSelf)
                _warningEffect.SetActive(true);

            else if (_warningLastTime <= _time && _time < _shotLastTime + _warningLastTime)
            {
                if (!_laserEffect.activeSelf && _warningEffect.activeSelf)
                {
                    _laserEffect.SetActive(true);
                    _warningEffect.SetActive(false);
                }
                laserShot(width);
            }
        }

        else
        {
            _laserEffect.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void laserDynamic(float width, Vector2 startPoint, Vector2 endPoint, float moveSpeed)
    {
      
        if (transform.position != (Vector3)startPoint && _ready == false)
        {
            transform.position = startPoint;
            _ready = true;
        }
           

        if (_time < _warningLastTime)
        {
            _time += Time.deltaTime;

            if (!_warningEffect.activeSelf)
                _warningEffect.SetActive(true);

        }

        else
        { 
            if(_warningEffect.activeSelf)
                _warningEffect.SetActive(false);


            if (!_laserEffect.activeSelf)       
                _laserEffect.SetActive(true);       
            
            laserShot(width);
            transform.position = Vector3.MoveTowards(transform.position, (Vector3)endPoint, Time.deltaTime * moveSpeed);
        }
          
        if(transform.position == (Vector3)endPoint)
        {
            _laserEffect.SetActive(false);
            gameObject.SetActive(false);
        }
       

    }

    public void setDamage(int dmg)
    {
        this._damage = dmg;
    }

    public void setDir() { }//�������� ����ü�� ��� �Ⱦ�
    public void lastLimit() { }//�������� ����ü�� ��� �Ⱦ�


}
