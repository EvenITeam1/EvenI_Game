using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public enum RENDERER_TYPE {
    DEFAULT= 0, CANVAS, PARTICLE, SPRITE, TILE
}

public class AutoSortingLayer : MonoBehaviour {
    public SORTING_LAYERS sorting_layer = SORTING_LAYERS.Default;
    public int sorting_order;
    
    [Header("Privates")]

    [SerializeField] RENDERER_TYPE renderer_type = RENDERER_TYPE.DEFAULT;
    [SerializeField] private Canvas canvas;
    [SerializeField] private ParticleSystemRenderer particle;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private TilemapRenderer tilemap;
    private void Awake() {
        if(transform.TryGetComponent<Canvas>(out canvas)){
            renderer_type = RENDERER_TYPE.CANVAS;
            return;
        }
        if(transform.TryGetComponent<ParticleSystemRenderer>(out particle)){
            renderer_type = RENDERER_TYPE.PARTICLE;
            return;
        }
        if(transform.TryGetComponent<SpriteRenderer>(out sprite)){
            renderer_type = RENDERER_TYPE.SPRITE;
            return;
        }
        if(transform.TryGetComponent<TilemapRenderer>(out tilemap)){
            renderer_type = RENDERER_TYPE.TILE;
            return;
        }
    }

    private void OnEnable() {
        //Debug.Log("Sorted");
        ChangeSortingLayerByPublic();
    }
    
    [ContextMenu("컴포넌트 불러오기")]
    public void SetComponents(){
        if(transform.TryGetComponent<Canvas>(out canvas)){
            renderer_type = RENDERER_TYPE.CANVAS;
            return;
        }
        if(transform.TryGetComponent<ParticleSystemRenderer>(out particle)){
            renderer_type = RENDERER_TYPE.PARTICLE;
            return;
        }
        if(transform.TryGetComponent<SpriteRenderer>(out sprite)){
            renderer_type = RENDERER_TYPE.SPRITE;
            return;
        }
        if(transform.TryGetComponent<TilemapRenderer>(out tilemap)){
            renderer_type = RENDERER_TYPE.TILE;
            return;
        }

    }

    [ContextMenu("소팅레이어 변경하기")]
    public void ChangeSortingLayerByPublic(){
        switch(renderer_type){
            case RENDERER_TYPE.CANVAS : {
                canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                canvas.sortingLayerName = sorting_layer.ToString();
                if(canvas.sortingOrder == 0) canvas.sortingOrder = sorting_order;
                break;
            }
            case RENDERER_TYPE.PARTICLE : {
                particle.sortingLayerName = sorting_layer.ToString();
                if(particle.sortingOrder == 0) particle.sortingOrder = sorting_order;
                break;
            }
            case RENDERER_TYPE.SPRITE : {
                sprite.sortingLayerName = sorting_layer.ToString();
                if(sprite.sortingOrder == 0) sprite.sortingOrder = sorting_order;
                break;
            }
            case RENDERER_TYPE.TILE : {
                tilemap.sortingLayerName = sorting_layer.ToString();
                if(tilemap.sortingOrder == 0) tilemap.sortingOrder = sorting_order;
                break;
            }
        }
    }
}