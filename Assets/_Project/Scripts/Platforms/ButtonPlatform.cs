using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ButtonPlatform : MonoBehaviour
    {
        [SerializeField] GameObject bridge;

        private int objectsInButton;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
            {
                objectsInButton++;
                ActivateBridge();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
            {
                objectsInButton--;
                if (objectsInButton <= 0)
                {
                    DeactivateBridge();
                }
            }
        }

        private void ActivateBridge()
        {
            bridge.SetActive(true);

        }

        private void DeactivateBridge()
        {
            bridge.SetActive(false);
        }
    }
}
