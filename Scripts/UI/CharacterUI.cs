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

    public static DOG_INDEX OnSelect;
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
        OnSelect = index;
        circleNameText.text = "<" + playerData.Character_engname + ">";
        refreshMoreInfoAndSelectButton();
    }

    void refreshMoreInfoAndSelectButton()
    {
        if (!isUnlocked)
        {
            selectButton.interactable = false;
            infoNameText.text = "???";
            infoeScriptText.text = "보유하지 않은 캐릭터입니다.";
            infoHpText.text = "상점에서";
            infoHpRecoverText.text = "캐릭터를";
            infoBasicAttText.text = "만나보세요";
            infoJumpAttText.text = "!!!!";         
        }

        else
        {
            selectButton.interactable = true;
            infoNameText.text = playerData.Character_korname;
            infoeScriptText.text = playerData.Character_script;
            infoHpText.text = $"체력 : {playerData.Character_hp}";
            var basicBulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(playerData.Character_bullet_index_1);
            var jumpBulletData = GameManager.Instance.BulletDataTableDesign.GetBulletDataByINDEX(playerData.Character_bullet_index_2);
            infoHpRecoverText.text = $"회복속도 : {playerData.Character_per_hp_heal}/s";
            infoBasicAttText.text = $"일반공격력 : {basicBulletData.Bullet_min_dmg} ~ {basicBulletData.Bullet_max_dmg}";
            infoJumpAttText.text = $"점프공격력 : {jumpBulletData.Bullet_min_dmg} ~ {jumpBulletData.Bullet_max_dmg}";        
        }    
    }

    public void ApplySelectCharacter()
    {
        GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedPlayerINDEX = OnSelect;
    }
}