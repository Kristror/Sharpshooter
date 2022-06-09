using Players;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField] public int _amount = 10;

        internal UnityAction<Transform, ItemType> _onDestroy;

        public void Init(UnityAction<Transform, ItemType> onDestroy)
        {
            _onDestroy = onDestroy;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<PlayerInventory>();
                if (player.Ammo != player.MaxAmmo)
                {
                    player.PickAmmo(_amount);
                    _onDestroy(gameObject.transform.parent, ItemType.Ammo);
                    Destroy(gameObject);
                }
            }
        }
    }
}
