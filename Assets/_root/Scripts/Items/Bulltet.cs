using UnityEngine;

namespace Items
{
    public class Bulltet : MonoBehaviour
    {
        [SerializeField] public float speed = 10;
        [SerializeField] public int damage = 25;
        [SerializeField] public float timeOfDes = 2.5f;

        private Rigidbody _rb;

        private void Awake()
        {
            Destroy(gameObject, timeOfDes);
            _rb = GetComponent<Rigidbody>();

            _rb.rotation = Quaternion.Euler(0, 0, 90);
            _rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<IEntity>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}