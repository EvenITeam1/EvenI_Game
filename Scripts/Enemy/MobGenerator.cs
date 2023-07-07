using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class MobGenerator : MonoBehaviour {
    public Transform[] GenFloor;
    public GameObject AlertObject;
    public float arrowposition;

    private void Awake() {
        foreach(Transform child in GenFloor){
            GameObject alert = Instantiate(AlertObject ,child.transform);
            alert.transform.localPosition = Vector2.right * arrowposition;
        }        
    }

    public void GenerateMobs(List<Mob> _mobList, List<float> _mobInvokePos, List<int> _FloorList){
        for(int i = 0; i < _FloorList.Count; i++){
            GenFloor[i].GetChild(0).gameObject.SetActive(true);
            Mob instantMob = Instantiate(_mobList[i], GenFloor[i]);
            instantMob.mobMoveData.invokePosition = _mobInvokePos[i];
            instantMob.gameObject.SetActive(true);
        }
    }
}