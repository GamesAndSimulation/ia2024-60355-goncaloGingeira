using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class HeartRotator : MonoBehaviour
    {
        
        [SerializeField] GameObject heart;
        [SerializeField] float speed;
        [SerializeField] private AudioSource healSFX;
        
        void Update ()
        {
            transform.RotateAround(heart.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        
        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Player"))
            {   
                healSFX.Play();
                heart.SetActive(false);
                collision.gameObject.GetComponent<PlayerController>().AddHealth();
            }
        }
        
        public void SetHeartActive()
        {
            heart.SetActive(true);
        }
    }
}