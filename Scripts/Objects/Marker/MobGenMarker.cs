using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenMarker : EventMarker 
{
    // public bool isActivated;
    // protected RaycastHit2D hit;
    public List<Mob> mobElements;
    public List<float> invokePosition;
    public List<int> genPositions;

    private void Awake() {
        if(mobElements == null){throw new System.Exception("리스트가 비어있습니다.");}
        if(genPositions == null){throw new System.Exception("리스트가 비어있습니다.");}

        if(mobElements.Count != genPositions.Count) {throw new System.Exception("몬스터와 위치의 개수가 동일하지 않음");}
        if(mobElements.Count != invokePosition.Count) {throw new System.Exception("몬스터와 보이는 위치의 개수가 동일하지 않음");}
    }
    protected override void FixedUpdate() {
        hit = GetCastedTarget();
        if(hit.collider.name == GlobalStrings.PLAYER_STRING && this.isActivated == false){
            this.isActivated = true;
            GameManager.Instance.GlobalMobGenerator.GenerateMobs(mobElements, invokePosition, genPositions);
        }
    }
}