using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CastleManager : MonoBehaviour
    {
        
        [SerializeField] GameObject openDoors;
        [SerializeField] GameObject closedDoors;
        [SerializeField] GameObject jailDoor;
        
        [SerializeField] Enemy Boss;
        [SerializeField] HealthBar bossHealthBar;
        
        float maxDistance = 40f;
        int maxHealth = 100;
        
        void Start()
        {
            bossHealthBar.SetMaxHealth(maxHealth);
            bossHealthBar.gameObject.SetActive(false);
        }

    
        void Update()
        {
            BossDefeated();
            ShowBossHealthBar();
            bossHealthBar.SetHealth((int)Boss.health);
        }
        
        void ShowBossHealthBar()
        {
            if (Boss != null)
            {
                if (Vector3.Distance(Boss.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= maxDistance && Boss.health > 0)
                {
                    bossHealthBar.gameObject.SetActive(true);
                }
                else
                {
                    bossHealthBar.gameObject.SetActive(false);
                }
            }
            
        }


        void BossDefeated()
        {
            if (Boss.health <= 0)
            {
                OpenDoors();
                jailDoor.SetActive(false);
            }
            
        }

        public void OpenDoors()
        {
            openDoors.SetActive(true);
            closedDoors.SetActive(false);
        }
        
        public void CloseDoors()
        {
            openDoors.SetActive(false);
            closedDoors.SetActive(true);
        }
    }
}
