using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyRigid = _enemy.GetComponent<Rigidbody2D>();
        enemyRigid.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
