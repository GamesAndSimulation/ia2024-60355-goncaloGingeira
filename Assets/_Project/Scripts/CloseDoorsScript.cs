using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CloseDoorsScript : MonoBehaviour
    {   
        
        [SerializeField] CastleManager castleManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                castleManager.CloseDoors();
            }
        }
    }
}
