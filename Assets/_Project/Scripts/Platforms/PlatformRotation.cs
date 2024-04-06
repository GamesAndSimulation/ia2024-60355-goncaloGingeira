using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlatformRotation : MonoBehaviour {
        
        [SerializeField] Transform centerPoint;
        [SerializeField] float rotationSpeed = 50f;

        void Update()
        {
            transform.RotateAround(centerPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
