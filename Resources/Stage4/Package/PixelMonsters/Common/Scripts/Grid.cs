using UnityEngine;

namespace Assets.PixelMonsters.Common.Scripts
{
    public class Grid : MonoBehaviour
    {
        public float CellSize;

        public void OnValidate()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.localPosition = new Vector3(i * CellSize - (transform.childCount - 1) / 2f * CellSize, 0, 0);
            }
        }
    }
}