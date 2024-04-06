using System.Collections;
using UnityEngine;

namespace Platformer
{
    public class DisappearingPlatform : MonoBehaviour
    {
        [SerializeField] float timeToDisappear = 1f;
        [SerializeField] float timeToAppear = 2f;

        BoxCollider platformCollider;
        MeshRenderer platformRenderer; 

        void Start()
        {
            platformCollider = GetComponent<BoxCollider>();
            platformRenderer = GetComponent<MeshRenderer>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DisappearingRoutine());
            }
        }

        IEnumerator DisappearingRoutine()
        {
            yield return new WaitForSeconds(timeToDisappear);

            platformCollider.enabled = false; 
            platformRenderer.enabled = false; 
            

            yield return new WaitForSeconds(timeToAppear);
            platformRenderer.enabled = true; 
            platformCollider.enabled = true; 
        }
    }
}