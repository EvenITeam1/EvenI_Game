using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetEnemyLaser : MonoBehaviour //일단 레이저에 대한 함수만 있어서 이름이 이런데,이후에 모든 발사체의 실행을 담당하는 코어 스크립트가 될수도 있음
{
    //싱글톤 패턴
    #region
    public static SetEnemyLaser instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        else
            Destroy(gameObject);
    }
    #endregion
    //싱글톤 패턴

    public void executeLaser(GameObject laser)
    {
        laser.SetActive(true);
    }
}
