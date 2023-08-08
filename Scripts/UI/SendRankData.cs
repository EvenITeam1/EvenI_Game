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

        Debug.Log("�ڷ�ƾ ����");
        StartCoroutine(Post(form));
    }
    private IEnumerator Post(WWWForm form)
    {
        const string url = "https://script.google.com/macros/s/AKfycbzjLMwnkoMN1m7MqHXfO9jXoiUardHIo5YojLedWv7dqGU1AZpmAycrp5WyQ0WnDF6NEw/exec";
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            Debug.Log("�ڷ�ƾ ���� ����");
            // �ش� url�� form�� ���� www��ü�� ���������
            // SendWebRequest�� ���� ���������Ʈ �������� doPost(e)��
            // �޽����� �����Ѵ�.
            yield return www.SendWebRequest();
            Debug.Log("�������� �Ϸ�");
            // �޽����� ���������� �ְ�޾������� �Ʒ� text�� �α׷� ��´�.
            if (www.isDone)
                print(www.downloadHandler.text);
            else
                print("Error");
        }
    }
}
