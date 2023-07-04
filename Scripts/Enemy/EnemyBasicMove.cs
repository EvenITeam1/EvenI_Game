using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicMove : MonoBehaviour
{
    [SerializeField] GameObject _parent;
    [SerializeField] PlayerMoveData _playerMoveData;

    void FixedUpdate()
    {
        _parent.transform.Translate(new Vector2(_playerMoveData.horizontal * _playerMoveData.speed, 0));
    }
}
