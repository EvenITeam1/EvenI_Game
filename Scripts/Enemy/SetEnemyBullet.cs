using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyBullet
{
    public static void fireBullet(Vector2 destination, GameObject bulletType, GameObject enemyObj)
    {
        GameObject bullet = ObjectPool.instance.GetObject(bulletType);
        bullet.transform.position = enemyObj.transform.position;
        bullet.transform.right = (Vector3)destination - enemyObj.transform.position;
        bullet.SetActive(true);
    }
}
