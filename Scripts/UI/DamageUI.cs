using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _alphaSpeed;
    public TextMeshPro _text;
    Color _alpha;
    public float _damage;
    float _time;
    [SerializeField] float _destroyTime;

    private void OnEnable()
    {
        _alpha = _text.color;
        _text.text = _damage.ToString();
        _time = 0;
    }

    void Update()
    {
        transform.Translate(new Vector3(0, _moveSpeed * Time.deltaTime, 0)); // �ؽ�Ʈ ���� �ö󰡵���

        _alpha.a = Mathf.Lerp(_alpha.a, 0, Time.deltaTime * _alphaSpeed); // �ؽ�Ʈ ���� ���������
        _text.color = _alpha;

        if (_time < _destroyTime)
            _time += Time.deltaTime;

        else
            ObjectPool.instance.ReturnObject(gameObject);
    }
}
