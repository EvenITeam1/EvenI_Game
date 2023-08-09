using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCallback : MonoBehaviour
{
    private bool isReturned;
    private void Awake() {
        isReturned = false;
    }
    private void OnParticleSystemStopped() {
        ObjectPool.instance.ReturnObject(gameObject);
        isReturned = true;
    }
    private void OnDisable(){
        if(!isReturned) return;
        ObjectPool.instance.ReturnObject(gameObject);
        isReturned = true;
    }
    private void OnDestroy() {
        if(!isReturned) return;
        ObjectPool.instance.ReturnObject(gameObject);
        isReturned = true;
    }
}