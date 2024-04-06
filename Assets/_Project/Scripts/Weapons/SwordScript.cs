using UnityEngine;

namespace Platformer
{
    public class SwordScript : MonoBehaviour
    {
        [SerializeField] float damage = 10f;
        
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }

        }
    }
}