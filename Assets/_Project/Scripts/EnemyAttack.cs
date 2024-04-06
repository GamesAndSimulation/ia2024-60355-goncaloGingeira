using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] AudioSource hitSFX;
        [SerializeField] float hitCooldown = 1.5f;
        bool canDamagePlayer = true;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && canDamagePlayer)
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(gameObject.tag);
                hitSFX.Play();
                StartCoroutine(HitCooldown());
            }
        }

        IEnumerator HitCooldown()
        {
            canDamagePlayer = false;

            yield return new WaitForSeconds(hitCooldown);

            canDamagePlayer = true;
        }

    }
}
