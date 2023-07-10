using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectAxisY : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out Bullet playerBullet))
        {
            ObjectPool.instance.ReturnObject(playerBullet.gameObject);
        }
    }
}
