using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickNameJsonData
{
    public string nickName {get;}
    public bool isFirstLogin {get;}

    public NickNameJsonData(bool isFirstLogin, string nickName)
    {
        this.isFirstLogin = isFirstLogin;
        this.nickName = nickName;
    }
}
