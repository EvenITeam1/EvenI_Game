using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetEnemyLaser : MonoBehaviour //�ϴ� �������� ���� �Լ��� �־ �̸��� �̷���,���Ŀ� ��� �߻�ü�� ������ ����ϴ� �ھ� ��ũ��Ʈ�� �ɼ��� ����
{
    //�̱��� ����
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
    //�̱��� ����

    public void executeLaser(GameObject laser)
    {
        laser.SetActive(true);
    }
}
