using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetEnemyLaser //일단 레이저에 대한 함수만 있어서 이름이 이런데,이후에 모든 발사체의 실행을 담당하는 코어 스크립트가 될수도 있음
{
    public static void executeLaser(GameObject laser)
    {
        laser.SetActive(true);
    }
}
