using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSelectedCharacterIcon : MonoBehaviour
{
   [SerializeField] List<Sprite> iconList;
    static Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();
        icon.sprite = iconList[(int)GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedPlayerINDEX - 1001];
    }
}
