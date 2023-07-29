using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public DOG_INDEX index;
    public PlayerData playerData;
    public bool isUnlocked;
    SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshProUGUI tmpro;
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
        Debug.Log("asdf");
        tmpro.text = "<" + playerData.Character_engname + ">";
    }
}
