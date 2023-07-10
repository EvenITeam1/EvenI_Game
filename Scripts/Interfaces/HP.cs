using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public interface HP
{ 
    public float getHP();
    public float getMaxHp();
    public void setHP(float hp);
    public bool isAlive();
    public void die();
}
