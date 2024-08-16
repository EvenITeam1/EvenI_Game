using Newtonsoft.Json;
using UnityEngine;
using System;
public class JsonTest : MonoBehaviour
{
    public TextAsset JsonFile;//Variable that contains json data
    private void Start()
    {
        var result = JsonConvert.DeserializeObject<TestClass002[]>(JsonFile.text);//convert to usable data
        foreach (var i in result)
        {
            //Debug.Log($"Id : {i.Id}, Value : {i.Value}, Bool : {i.Boolean}");//Print data;
        }
    }
}

[Serializable]
public class TestClass002
{
    public int Id;
    public int Value;
    public bool Boolean;
}

