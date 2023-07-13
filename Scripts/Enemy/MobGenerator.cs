using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class MobGenerator : MonoBehaviour {
    public Transform InstantTransform;
    public GameObject AlertObject;
    public float arrowposition;

    public void GenerateMobs(List<MobGenData> _mobGenDatas){
        foreach(MobGenData item in _mobGenDatas){
            Mob[] mobs = item.mobs;
            MobMoveData[] mobMoveDatas = item.mobMoveData;
            StartCoroutine(AsyncGenerateSubMobs(mobs, mobMoveDatas, item.gap));
        }
    }
    
    IEnumerator AsyncGenerateSubMobs(Mob[] _mobs, MobMoveData[] _mobMoveDatas, float _gapTime) {
        for(int i = 0; i < _mobs.Length; i++){
            yield return YieldInstructionCache.WaitForSeconds(_gapTime);
            Mob instantMob = Instantiate(_mobs[i], InstantTransform);
            instantMob.transform.localPosition = Vector2.up * _mobMoveDatas[i].invokePosition.y;
            instantMob.mobMoveData = _mobMoveDatas[i];
            instantMob.IsInstantiatedFirst = true;
            instantMob.gameObject.SetActive(true);
        }
    }

    public void GenerateAlertObject(Vector2 instantPos){
        GameObject alertObj = Instantiate(AlertObject, InstantTransform);
        alertObj.transform.localPosition = instantPos;
    }
}