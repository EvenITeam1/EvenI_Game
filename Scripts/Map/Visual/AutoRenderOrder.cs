using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace TwoDimensions
{
    public class AutoRenderOrder : MonoBehaviour {
        [SerializeField] List<SpriteRenderer> spriteRenderer = new List<SpriteRenderer>();

        public void GetChildSpriteRenderers(){
            foreach(Transform items in transform){
                spriteRenderer.Add(items.GetComponent<SpriteRenderer>());
            }
            SetRenderOrder((int)transform.localPosition.z);
        }

        private void Awake() {
        }

        public void SetRenderOrder(int _order){
            foreach(SpriteRenderer sr in spriteRenderer){
                sr.sortingOrder = -_order;
            }
        }
        public void AddRenderOrder(int _amount){
            foreach(SpriteRenderer sr in spriteRenderer){
                sr.sortingOrder -= _amount;
            }
        }
    }
}
