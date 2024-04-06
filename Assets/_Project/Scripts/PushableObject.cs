using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PushableObject : MonoBehaviour {
        
        public float pushForce = 10f;
        private Rigidbody rb;
        private Vector3 originalPosition;
        [SerializeField] WaterChecker waterChecker;
        
        
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            originalPosition = transform.position;
        }

        void Update() {
          ResetObject();  
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector3 pushDirection = transform.position - collision.transform.position;

                rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
            }
        }
        
        void ResetObject() {
            if (waterChecker.IsInWater) {
                rb.velocity = Vector3.zero;
                transform.position = originalPosition;
            }
        }
    }
}
