using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBossMove
{
   
    public static void move(float x, float y, float speed, GameObject enemy, GameObject flag)
    {
        Vector3 localDest = new Vector3(x, y, 0);
        flag.transform.position = enemy.transform.position + localDest;
        flag.SetActive(true);
        var enemyRigid = enemy.GetComponent<Rigidbody2D>();
        enemyRigid.velocity = (Vector2)localDest.normalized * speed;   
    }

    public static float goUp(float y, float speed, GameObject enemy, GameObject flag)
    {
        move(0, y, speed, enemy, flag);
        return y / speed;

    }
    public static float goDown(float y, float speed, GameObject enemy, GameObject flag)
    {
        move(0, -y, speed, enemy, flag);
        return y / speed;

    }
    public static float goLeft(float x, float speed, GameObject enemy, GameObject flag)
    {
        move(-x, 0, speed, enemy, flag);
        return x / speed;
    }
    public static float goRight(float x, float speed, GameObject enemy, GameObject flag)
    {
        move(x, 0, speed, enemy, flag);
        return x / speed;
    }
}
