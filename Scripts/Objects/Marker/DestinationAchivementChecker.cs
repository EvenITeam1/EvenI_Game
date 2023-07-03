using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwoDimensions
{
    public class DestinationAchivementChecker : MonoBehaviour
    {
        [SerializeField] Transform StartTransform;
        [SerializeField] Transform EndTransform;
        [SerializeField] Transform Player;

        public float MapDistance;

        private void Awake()
        {
            MapDistance = Mathf.Round(EndTransform.position.x - StartTransform.position.x);
        }

        public float GetRunningAmount()
        {
            return StartTransform.position.x - Player.position.x;
        }
        public float GetRemainAmount()
        {
            return EndTransform.position.x - GetRemainAmount();
        }

        public float GetRatioOfDistance()
        {
            return GlobalFunction.GetTwoValueRatio(MapDistance, Player.position.x) * 100;
        }
    }
}