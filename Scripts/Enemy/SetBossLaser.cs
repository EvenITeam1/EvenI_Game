using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetBossLaser //�ϴ� �������� ���� �Լ��� �־ �̸��� �̷���,���Ŀ� ��� �߻�ü�� ������ ����ϴ� �ھ� ��ũ��Ʈ�� �ɼ��� ����
{
    public static void executeLaser(GameObject laser)
    {
        GameManager.Instance.GlobalSoundManager.PlaySFXByString("SFX_Enemy_Laser");
        laser.SetActive(true);
    }
}
