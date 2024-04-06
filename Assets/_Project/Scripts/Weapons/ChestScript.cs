using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Platformer
{
    public class ChestScript : MonoBehaviour {
        
        [SerializeField] GameObject chest;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] GameObject player;
        [SerializeField] GameObject swordChest;
        [SerializeField] PlayerController playerController;
        
        float maxDistanceText = 22f;
        float maxDistanceSword = 14f;
        
        bool swordObtained;
        
        
        void Start() {
            text.gameObject.SetActive(false);
        }
        
        
        void Update() {
            DetectPlayer();
            ObtainSword();
            
        }


        void DetectPlayer() {
            if (Vector3.Distance(player.transform.position, transform.position) <= maxDistanceText && !swordObtained) {
                text.gameObject.SetActive(true);
            }
            else {
                text.gameObject.SetActive(false);
            }
        }
        
        void ObtainSword() {
            if (Vector3.Distance(player.transform.position, transform.position) <= maxDistanceSword && !swordObtained) {
                
                if(Input.GetKeyDown(KeyCode.E)) {
                    swordChest.SetActive(false);
                    playerController.ToggleSwordVisibility();
                    swordObtained = true;
                }
                
            }
        }
    }
}
