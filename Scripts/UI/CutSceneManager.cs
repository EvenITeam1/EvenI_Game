using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CutSceneManager : MonoBehaviour, IPointer​Click​Handler
{
    public Image            imageComponent;
    public TextMeshProUGUI  cutSceneNumertext;
    public List<Sprite>     imagesQueue;
    private int currentNum = 1;

    private void OnEnable(){
    }
    
    public void OnPointerClick(PointerEventData pointerEventData){
        RunnerManager.Instance.GlobalEventInstance.IsGamePaused = true;
        if(imagesQueue.Count != 0){
            currentNum++;
            cutSceneNumertext.text = currentNum.ToString();
            imageComponent.sprite = imagesQueue.First();
            imagesQueue.RemoveAt(0);
        }
        else {
            RunnerManager.Instance.GlobalEventInstance.IsGamePaused = false;
            Destroy(gameObject);
        }
    }
}
