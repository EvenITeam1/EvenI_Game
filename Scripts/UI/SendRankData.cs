using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendRankData : MonoBehaviour
{
    public void SendRaidScoreToGoogleSpreadSheet()
    {
        Debug.Log("send1");
        DOG_INDEX selectedDogIndex = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedPlayerINDEX;
        PlayerData playerData = GameManager.Instance.CharacterDataTableDesign.GetPlayerDataByINDEX(selectedDogIndex);
        string characterName = playerData.Character_korname;
        int score = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().ingameSaveData.CollectedScore;

        WWWForm form = new WWWForm();
        form.AddField("nickname", "asdf");
        form.AddField("character", "fdas");
        form.AddField("score", 10);
        form.AddField("time", 20);

        Debug.Log("코루틴 직전");
        StartCoroutine(Post(form));
    }
    private IEnumerator Post(WWWForm form)
    {
        const string url = "https://script.google.com/macros/s/AKfycbzjLMwnkoMN1m7MqHXfO9jXoiUardHIo5YojLedWv7dqGU1AZpmAycrp5WyQ0WnDF6NEw/exec";
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            Debug.Log("코루틴 내부 진입");
            // 해당 url에 form을 담은 www객체가 만들어졌고
            // SendWebRequest를 통해 스프레드시트 편집기의 doPost(e)로
            // 메시지를 전송한다.
            yield return www.SendWebRequest();
            Debug.Log("정보전달 완료");
            // 메시지가 정상적으로 주고받아졌으면 아래 text를 로그로 찍는다.
            if (www.isDone)
                print(www.downloadHandler.text);
            else
                print("Error");
        }
    }
}
