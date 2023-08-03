using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class NickName : MonoBehaviour
{
    public static string Nickname { get; }

    void saveNickNameDataToJson()
    {
        var result = JsonConvert.SerializeObject(Nickname);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.dataPath, "NickNameDat"), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(result);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
}
