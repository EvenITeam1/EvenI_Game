using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCharacterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI selectedDogName;
    [SerializeField] DOG_INDEX selectedDogIndex;
    [SerializeField] List<string> dogNameList;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        selectedDogIndex = GameManager.Instance.GlobalSaveNLoad.GetSaveDataByRef().outgameSaveData.SelectedPlayerINDEX;
        animator.SetInteger("Index", (int)selectedDogIndex - 1000);
       
        selectedDogName.text = "<" + dogNameList[(int)selectedDogIndex - 1001] + ">";
    }
}
