using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendRankData : MonoBehaviour
{
    public void SendRaidScoreToGoogleSpreadSheet()
    {
        DOG_INDEX selectedDogIndex = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedPlayerINDEX;
        PlayerData playerData = GameManager.Instance.CharacterDataTableDesign.GetPlayerDataByINDEX(selectedDogIndex);
        string characterName = playerData.Character_korname;
        int score = (int)RunnerManager.Instance.GlobalEventInstance.scoreCheck.Score;
        if (score > 46500)
            score = 46500;
        WWWForm form = new WWWForm();
        form.AddField("nickname", NickName.Nickname);
        form.AddField("character", characterName);
        form.AddField("score", score);
        form.AddField("timeMin", (int) (Timer.time / 60));
        float truncatedFloat = Mathf.Floor((Timer.time % 60) * 1000f) / 1000f;
        form.AddField("timeSec", truncatedFloat.ToString());
        StartCoroutine(Post(form));
    }
    private IEnumerator Post(WWWForm form)
    {
        const string url = "https://script.google.com/macros/s/AKfycbydoaMciSI2AgScB1gI5wWBVC1VjzlnBTjLKhFfUeqNAcQjJ8PbthqFtSarG7gCsldV/exec";
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            // 해당 url에 form을 담은 www객체가 만들어졌고
            // SendWebRequest를 통해 스프레드시트 편집기의 doPost(e)로
            // 메시지를 전송한다.
            yield return www.SendWebRequest();
            // 메시지가 정상적으로 주고받아졌으면 아래 text를 로그로 찍는다.
            if (www.isDone)
                print(www.downloadHandler.text);
            else
                print("Error");
        }
    }
}
