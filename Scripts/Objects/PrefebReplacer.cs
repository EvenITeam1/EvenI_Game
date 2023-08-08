using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefebReplacer : MonoBehaviour
{
    public string TargetPrefebName;
    public GameObject ReplaceObject;
    [ContextMenu("Replace Prefeb By String And Object")]
    public void Replace(){
        #if UNITY_EDITOR
        foreach(Transform E in transform){
            if(E.name == TargetPrefebName){
                Transform replaceTransform = E;
                var cloneObj = Instantiate(ReplaceObject, transform);
                cloneObj.name = ReplaceObject.name;
                cloneObj.transform.position = replaceTransform.position;
                cloneObj.transform.rotation = replaceTransform.rotation;
                cloneObj.transform.localScale = replaceTransform.localScale;
                DestroyImmediate(E.gameObject);
            }
        }
        #endif
    }
}
