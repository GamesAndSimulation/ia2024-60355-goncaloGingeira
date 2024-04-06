using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GroundChecker : MonoBehaviour {
        
        [SerializeField] LayerMask groundLayers;
        [SerializeField] float groundDistance = 0.2f; // need to confirm this value

        public bool IsGrounded;

        void Update()
        {
            IsGrounded = Physics.Raycast(transform.position, Vector3.down, out _,  groundDistance, groundLayers);
        }
        
        
    }
}
