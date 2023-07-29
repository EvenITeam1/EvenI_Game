using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public DOG_INDEX index;
    public PlayerData playerData;
    public bool isUnlocked;
    SpriteRenderer spriteRenderer;
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
}
