using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class SpeedUpMarker : MonoBehaviour{

    [SerializeField] private string inputString;
    [SerializeField] UnityEvent<string> PrintString;
    
    [SerializeField] UnityEvent OnTriggerEvents;
    public bool isActivated;
    protected RaycastHit2D[] hits;

    protected void Awake(){

    }
    protected void FixedUpdate() {
        hits = GetCastedTarget();
        var player = (
            from hit in hits  
            where (hit.collider.tag == GlobalStrings.LAYERS_STRING[(int)PROJECT_LAYERS.Player])
            select hit
        );

        if(player.Count() == 0) {isActivated = false;}
        else if(player.Count() != 0 && !isActivated)
        {
            isActivated = true;
            player.Any((E) => {
                if(E.transform.TryGetComponent(out Player playerComponent)) {
                    playerComponent.SpeedUp();
                    OnTriggerEvents.Invoke();
                    return true;
                }
                return false;
            });
        }
    }
    protected virtual RaycastHit2D[] GetCastedTarget()
    {
        return hits = Physics2D.RaycastAll(transform.position, Vector2.down, 300f);
    }
}