using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetNickName : MonoBehaviour
{
    private void FixedUpdate()
    {
        TextMeshProUGUI TM = GetComponent<TextMeshProUGUI>();
        TM.text = NickName.Nickname;
    }
}
