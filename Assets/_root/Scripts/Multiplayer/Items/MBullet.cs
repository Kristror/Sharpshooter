using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

namespace Multiplayer
{
    public class MBullet : MonoBehaviourPunCallbacks
    {
        [SerializeField] public float speed = 10;
        [SerializeField] public int damage = 20;
        [SerializeField] public float timeOfDes = 2.5f;

        private Rigidbody _rb;
        string _owner;

        public void SetOwner(string owner)
        {
            _owner = owner;
            Shoot();
        }

        private void Shoot()
        {
            Destroy(gameObject, timeOfDes);
            _rb = GetComponent<Rigidbody>();

            _rb.rotation = Quaternion.Euler(0, 0, 90);
            //_rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name != _owner)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    collision.gameObject.GetComponent<MPlayerInventory>().TakeDamage(damage,_owner);
                }
                Destroy(gameObject);
            }
        }
    }
}