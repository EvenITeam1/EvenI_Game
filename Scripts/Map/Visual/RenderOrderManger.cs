using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace TwoDimensions
{
    public class RenderOrderManger : MonoBehaviour {
        List<AutoRenderOrder> settingTargets = new List<AutoRenderOrder>();

        private void Awake() {
            settingTargets = GetComponentsInChildren<AutoRenderOrder>().ToList();
        }
        private void Start() {
            foreach(AutoRenderOrder aro in settingTargets){
                aro.AddRenderOrder((int)transform.position.z);
            }
        }
    }
}