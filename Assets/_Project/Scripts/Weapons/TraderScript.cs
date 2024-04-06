using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platformer
{
    public class TraderScript : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] GameObject player;
        [SerializeField] GameObject shield;
        [SerializeField] PlayerController playerController;
        [SerializeField] CoinManager coinManager;
        [SerializeField] string purchaseMessage = "Want to buy this shield?";
        [SerializeField] string notEnoughCoinsMessage = "Not enough coins!";
        [SerializeField] string goodLuckMessage = "Good Luck!";

        [SerializeField] KeyCode purchaseKey = KeyCode.E;
        [SerializeField] int shieldPrice = 4;

        float maxDistanceText = 160f;
        bool shieldObtained;
        bool playerInRange;

        void Start()
        {
            text.gameObject.SetActive(false);
        }

        void Update()
        {   
            DetectPlayer();
            
            DisplayMessage(shieldObtained ? goodLuckMessage : purchaseMessage);

            if (playerInRange && !shieldObtained)
            {
                if (Input.GetKeyDown(purchaseKey))
                {
                    if (coinManager.coinsCollected == shieldPrice)
                    {   
                        ObtainShield();

                    }
                    else
                    {
                        DisplayMessage(notEnoughCoinsMessage);
                    }
                }
            }
            
        }


        void DetectPlayer()
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= maxDistanceText)
            {
                text.gameObject.SetActive(true);
                playerInRange = true;
            }
            else
            {
                text.gameObject.SetActive(false);
                playerInRange = false;
            }
        }

        void ObtainShield()
        {
            shield.SetActive(false);
            playerController.ToggleShieldVisibility();

            shieldObtained = true;

            coinManager.SpendCoins();
            

        }

        void DisplayMessage(string message)
        {
            text.text = message;
        }
    }
}
