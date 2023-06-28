using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, Hit
{
    public LayerMask _enemyLayer;
    public int _minDmg;
    public int _maxDmg;
    int _damage;
    public float _critChance;
    public float _critConst;//ũ��Ƽ�� ����
    float _time;
    public float _lastTime;
    Vector2 _moveDir;
    [SerializeField] Rigidbody2D _bulletRigid;
    public float _bulletSpeed;
    [SerializeField] GameObject _dmgPrefab;
    [SerializeField] int _critFontSize;
    [SerializeField] int _nonCritFontSize;
    [SerializeField] Color32 _critColor;
    [SerializeField] Color32 _nonCritColor;

    void Update()
    {
        lastLimit();
    }

    void OnEnable()
    {
        _time = 0;
        setDamage(Random.Range(_minDmg, _maxDmg + 1));
        setDir();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHP>())
        {
            getDamage(collision.gameObject);
            ObjectPool.instance.ReturnObject(gameObject);
        }
    }
    public void getDamage(GameObject obj)//obj�� �������� ���ϴ� �Լ�
    {
        EnemyHP enemyScript = obj.GetComponent<EnemyHP>();
        float currentHp = enemyScript.getHP();
        float actualDmg = _damage;
        var dmgPrefab = ObjectPool.instance.GetObject(_dmgPrefab);
        var dmgScript = dmgPrefab.GetComponent<DamageUI>();

        if (isCrit())//ũ��Ƽ���� ������ ���������� �� �������� ũ��Ƽ�� ������ �����ش�.
        {
            actualDmg *= _critConst;
            dmgScript._text.color = _critColor;
            dmgScript._text.fontSize = _critFontSize;
        }

        else 
        {
            dmgScript._text.color = _nonCritColor;
            dmgScript._text.fontSize = _nonCritFontSize;
        }

       
        dmgScript._damage = actualDmg;
        dmgPrefab.transform.position = transform.position;
        dmgPrefab.SetActive(true);

        enemyScript.setHP(currentHp - actualDmg);
        enemyScript.updateHpBar();
        if (!enemyScript.isAlive())
        {
            enemyScript.die();
        }
    }

    public void setDamage(int dmg)//����ü�� �������� ���ϴ� �Լ�
    {
        _damage = dmg;
    }

    public void setDir()//����ü�� ���� ������ ���ϴ� �Լ�
    {
        _moveDir = new Vector2(1, 0);
        _bulletRigid.velocity = _moveDir * _bulletSpeed;
    }

    public void lastLimit() //������ ���ӽð�(_lastTime)�� ����ϸ� ����ü�� ������ �����ϴ� �Լ�
    {
       _time += Time.deltaTime;

        if (_time >= _lastTime)
        {
            ObjectPool.instance.ReturnObject(gameObject);
        }        
    }
    public bool isCrit()//������ ũ��Ƽ�� Ȯ��(_critChance)�� ���� ũ��Ƽ�� ���� �ƴ����� ��ȯ�ϴ� �Լ�
    {
        if (Random.Range(0.0f, 1.0f) < _critChance)
        {
            return true;
        }

        else
            return false;
    }

}
