using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class Game_PL_Character_DataTable_design : MonoBehaviour {
    public List<PlayerData> playerDataforms = new List<PlayerData>();

    [ContextMenu("구글 스프레드 시트 로딩")]
    public void LoadDataFromSheet(){

    }
}