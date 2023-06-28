using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hit
{
    public void getDamage(GameObject obj);
    public void setDamage(int dmg);

    public void setDir();

    public void lastLimit();
}
