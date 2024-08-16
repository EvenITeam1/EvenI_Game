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
            // �ش� url�� form�� ���� www��ü�� ���������
            // SendWebRequest�� ���� ���������Ʈ �������� doPost(e)��
            // �޽����� �����Ѵ�.
            yield return www.SendWebRequest();
            // �޽����� ���������� �ְ�޾������� �Ʒ� text�� �α׷� ��´�.
            if (www.isDone)
                print(www.downloadHandler.text);
            else
                print("Error");
        }
    }
}
