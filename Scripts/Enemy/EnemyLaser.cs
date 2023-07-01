using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLaser : MonoBehaviour, Hit //laserObject �� �������� �������� �������� �߻��ϴ� �ڵ� laserObject ��ġ ������ setLaser
{
    [SerializeField] bool _isStatic;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] int _damage;
    [SerializeField] int _setDmg;
    [SerializeField] float _width;
    [SerializeField] float _warningLastTime;
    [SerializeField] GameObject _warningEffect;
    [SerializeField] GameObject _laserEffect;
    float _time;


    [SerializeField] float _shotLastTime;
    [SerializeField] Vector2 _shotPoint;


    [SerializeField] Vector2 _startPoint;
    [SerializeField] Vector2 _endPoint;
    [SerializeField] float _moveSpeed;

    private void Awake()
    {
        setDamage(_setDmg);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _time = 0; 
    }
    private void Update()
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
        Vector2 rightDown = objPos + new Vector2(0, width /2);
        var targets = Physics2D.OverlapAreaAll(leftUp, rightDown, _playerLayer);
        var targetN = targets.Length;

        for(int i = 0; i<targetN; i++)
        {
            if (targets[i].gameObject.GetComponent<PlayerHP>())
            {
                getDamage(targets[i].gameObject);
                Debug.Log("���ݹ޾Ҵ�.");
            }
        } 
    }

    public void laserStatic(float width, Vector2 shotPoint)
    {
        transform.position = (Vector3)shotPoint;
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
            {
                _laserEffect.SetActive(true);
                transform.position = Vector3.MoveTowards((Vector3)startPoint, (Vector3)endPoint, Time.deltaTime * moveSpeed);
            }
            laserShot(width);
        }
          
        if(transform.position == (Vector3)endPoint)
        {
            _laserEffect.SetActive(false);
            gameObject.SetActive(false);
        }
       

    }

    public void getDamage(GameObject obj)
    {
        PlayerHP playerScript = obj.GetComponent<PlayerHP>();
        float currentHp = playerScript.getHP();
        playerScript.setHP(currentHp - _damage);
        if (!playerScript.isAlive())
        {
            playerScript.die();
        }
    }

    public void setDamage(int dmg)
    {
        this._damage = dmg;
    }

    public void setDir() { }//�������� ����ü�� ��� �Ⱦ�
    public void lastLimit() { }//�������� ����ü�� ��� �Ⱦ�


}
