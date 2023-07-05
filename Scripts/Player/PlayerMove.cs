using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D _playerRigid;
    [SerializeField] Vector2 _moveDir;
    public float _speed;

    private void Update()
    {
        playerMove();
    }
   
    void playerMove()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            _moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        else
            _moveDir = Vector2.zero;

        _playerRigid.velocity = _moveDir * _speed;
    }
}
