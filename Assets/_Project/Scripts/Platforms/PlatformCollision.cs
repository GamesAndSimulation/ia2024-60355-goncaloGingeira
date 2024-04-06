using UnityEngine;

namespace Platformer
{
    public class PlatformCollision : MonoBehaviour
    {
        private Transform platform;
        private Transform originalParent;

        void Start()
        {
            originalParent = transform.parent;
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                ContactPoint contact = other.GetContact(0);
                if (contact.normal.y < 0.5f) return;

                platform = other.transform;
                transform.SetParent(platform);
            }
        }

        void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(originalParent);
                platform = null;
            }
        }
    }
}