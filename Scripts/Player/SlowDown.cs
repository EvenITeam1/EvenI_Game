using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public bool bossInComing = false;
    [SerializeField] GameObject _sideMirror;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerHP>())
        {
            Debug.Log("������ ����!");
            bossInComing = true;
            _sideMirror.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
