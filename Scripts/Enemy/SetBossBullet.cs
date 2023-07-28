using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBossBullet
{
    /// <summary>
    /// Shot bullet to destination with selected bulletType
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="bulletType"></param>
    /// <param name="enemyObj"></param>
    public static void fireBullet(Vector2 destination, GameObject bulletType, GameObject enemyObj)
    {
        GameObject bullet = ObjectPool.instance.GetObject(bulletType);
        bullet.transform.position = enemyObj.transform.position;
        bullet.transform.right = (Vector3)destination - enemyObj.transform.position;
        bullet.SetActive(true);
    }


    /// <summary>
    /// Shot bullet to local position (-10, y) by enemy with selected bulletType
    /// </summary>
    /// <param name="y"></param>
    /// <param name="bulletType"></param>
    /// <param name="enemyObj"></param>
    public static void fireBulletLocal(float y, GameObject bulletType, GameObject enemyObj)
    {
        fireBullet(new Vector2(-10, y) + (Vector2)enemyObj.transform.position, bulletType, enemyObj);
    }

    /// <summary>
    /// Shot bullet to random y position [min, max] with selected bulletType. (Local x position is -10)
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="bulletType"></param>
    /// <param name="enemyObj"></param>
    public static void fireBulletRandomPos(int min, int max, GameObject bulletType, GameObject enemyObj)
    {
        int n = Random.Range(min, max + 1);

        fireBulletLocal(n, bulletType, enemyObj);
    }

    public static void fireBulletRandomPos(float min, float max, GameObject bulletType, GameObject enemyObj)
    {
        float n = Random.Range(min, max + 1);

        fireBulletLocal(n, bulletType, enemyObj);
    }

}
