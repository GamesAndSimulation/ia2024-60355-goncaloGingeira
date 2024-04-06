using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class WaterChecker : MonoBehaviour {
        
        [SerializeField] LayerMask waterLayers;
        [SerializeField] float waterDistance = 0.08f;

        public bool IsInWater;

        void Update()
        {
            IsInWater = Physics.Raycast(transform.position, Vector3.down, out _,  waterDistance, waterLayers);
        }
    }
}
