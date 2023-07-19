using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace TwoDimensions
{
    public class AutoRenderOrder : MonoBehaviour {
        [HideInInspector] List<SpriteRenderer> spriteRenderer = new List<SpriteRenderer>();

        public void GetChildSpriteRenderers(){
            spriteRenderer.Add(transform.GetComponent<SpriteRenderer>());
            foreach(Transform items in transform){
                spriteRenderer.Add(items.GetComponent<SpriteRenderer>());
            }
        }

        public void SetRenderOrder(){
            foreach(SpriteRenderer sr in spriteRenderer){
                sr.sortingOrder = -(int)transform.localPosition.z;
            }
        }

        private void Start() {
            GetChildSpriteRenderers();
            SetRenderOrder();
        }

        [ContextMenu("순서 정리하기")]
        public void SetRenderOrderByContexMenu(){
            GetChildSpriteRenderers();
            SetRenderOrder();
        }
    }
}
