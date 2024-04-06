using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class CoinManager : MonoBehaviour
    {
        
        public int coinsCollected = 0;
        public Text coinText;
        
        void Start()
        {
            coinText = GameObject.Find("NumberOfCoins").GetComponent<Text>();
            UpdateCoinText();
        }

        void UpdateCoinText()
        {
            coinText.text = coinsCollected + " / 4";
        }
        
        public void AddCoins()
        {
            coinsCollected++;
            UpdateCoinText(); 
        }
        

        public void SpendCoins()
        {
            coinsCollected = 0;
            UpdateCoinText();
        }
    }
}
