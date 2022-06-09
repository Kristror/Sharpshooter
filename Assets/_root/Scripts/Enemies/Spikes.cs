using UnityEngine;

namespace Enemies
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] public int damage = 5;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
                collision.gameObject.GetComponent<IEntity>().TakeDamage(damage);
        }
    }
}