using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetEnemyLaser //�ϴ� �������� ���� �Լ��� �־ �̸��� �̷���,���Ŀ� ��� �߻�ü�� ������ ����ϴ� �ھ� ��ũ��Ʈ�� �ɼ��� ����
{
    public static void executeLaser(GameObject laser)
    {
        laser.SetActive(true);
    }
}
