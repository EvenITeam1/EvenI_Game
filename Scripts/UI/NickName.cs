using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using TMPro;
using DG.Tweening;

public class NickName : MonoBehaviour
{
    public static string Nickname { get; private set; }
    public static bool isFirstLogin { get; private set; }
    static bool isFirstAccess = true;
    [SerializeField] TextMeshProUGUI input;
    [SerializeField] GameObject nickNameCanvas;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject checkPopUpCanvas;
    [SerializeField] TextAsset JsonFile;
    [SerializeField] TextMeshProUGUI secondPopUpText;
    void Awake()
    {
        if(isFirstAccess)
        {
            LoadNickNameDataFromJson();
        }

        if (isFirstLogin == true && isFirstAccess == true)
        {
            nickNameCanvas.SetActive(true);
            isFirstAccess = false; 
        }
           
    }

    public void InputCheck()
    {
        //0~32 > 33~47 >58~64 >91~96 >123~127
        string InputText = input.text;
        int cnt = 0;
        char[] chars = InputText.ToCharArray();
        for (int i = 0; i <  InputText.Length; i++)
        {
            if(!(0<= chars[i] && chars[i] <= 47 || 58 <= chars[i] && chars[i] <= 64 || 91 <= chars[i] && chars[i] <= 96 || 123 <= chars[i] && chars[i] <= 127))
            {
                cnt++;               
            }
        }

        if(cnt == InputText.Length && 2<= InputText.Length - 1 && InputText.Length - 1 <= 6)
        {
            secondPopUpText.text = input.text;
            checkPopUpCanvas.SetActive(true);
        }

        else
        {
            panel.transform.DOShakePosition(1, 15);
        }
    }

    public void SaveNickNameDataToJson()
    {
        Nickname = input.text;
        isFirstLogin = false;
        NickNameJsonData nickNameJsonData = new NickNameJsonData(isFirstLogin, Nickname);
        var result = JsonConvert.SerializeObject(nickNameJsonData);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.dataPath, "NickNameJsonData"), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(result);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void LoadNickNameDataFromJson()
    {
        NickNameJsonData data = JsonConvert.DeserializeObject<NickNameJsonData>(JsonFile.text);
        Nickname = data.nickName;
        isFirstLogin = data.isFirstLogin;
    }
}
