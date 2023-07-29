using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public DOG_INDEX index;
    public PlayerData playerData;
    public bool isUnlocked;
    SpriteRenderer spriteRenderer;
    [SerializeField] Button selectButton;
    [SerializeField] TextMeshProUGUI circleNameText;

    [SerializeField] TextMeshProUGUI infoNameText;
    [SerializeField] TextMeshProUGUI infoeScriptText;
    [SerializeField] TextMeshProUGUI infoHpText;
    [SerializeField] TextMeshProUGUI infoHpRecoverText;
    [SerializeField] TextMeshProUGUI infoBasicAttText;
    [SerializeField] TextMeshProUGUI infoJumpAttText;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        refreshState();
        playerData = GameManager.Instance.CharacterDataTableDesign.GetPlayerDataByINDEX(index);//getDataFromDataTable
    }

    void refreshState()
    {
        if (isUnlocked)
            spriteRenderer.color = Color.white;

        else
            spriteRenderer.color = Color.black;
           
    }

    private void OnTriggerEnter(Collider other)
    {
        circleNameText.text = "<" + playerData.Character_engname + ">";
        refreshMoreInfoAndSelectButton();
    }

    void refreshMoreInfoAndSelectButton()
    {
        if (!isUnlocked)
        {
            selectButton.interactable = false;
            infoNameText.text = "???";
            infoeScriptText.text = "�������� ���� ĳ�����Դϴ�.";
            infoHpText.text = "��������";
            infoHpRecoverText.text = "ĳ���͸�";
            infoBasicAttText.text = "����������";
            infoJumpAttText.text = "!!!!";         
        }

        else
        {
            selectButton.interactable = true;
            infoNameText.text = playerData.Character_korname;
            infoeScriptText.text = playerData.Character_script;
            infoHpText.text = $"ü�� : {playerData.Character_hp}";
            var basicBulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(playerData.Character_bullet_index_1);
            var jumpBulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(playerData.Character_bullet_index_2);
            infoHpRecoverText.text = $"ȸ���ӵ� : {playerData.Character_per_hp_heal}/s";
            infoBasicAttText.text = $"�Ϲݰ��ݷ� : {basicBulletData.Bullet_min_dmg} ~ {basicBulletData.Bullet_max_dmg}";
            infoJumpAttText.text = $"�������ݷ� : {jumpBulletData.Bullet_min_dmg} ~ {jumpBulletData.Bullet_max_dmg}";        
        }    
    }
}
