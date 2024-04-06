using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CoinRotator : MonoBehaviour
    {   
        [SerializeField] GameObject coin;
        [SerializeField] float speed;
        [SerializeField] CoinManager coinManager;
        [SerializeField] private AudioSource coinSFX;

        
        void Update ()
        {
            transform.RotateAround(coin.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        
        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Player"))
            {   
                coinSFX.Play();
                coinManager.AddCoins();
                coin.SetActive(false);
            }
        }
    }
}
