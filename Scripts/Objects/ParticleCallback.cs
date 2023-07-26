using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCallback : MonoBehaviour
{
    private void OnParticleSystemStopped() {
        ObjectPool.instance.ReturnObject(gameObject);
    }
}
