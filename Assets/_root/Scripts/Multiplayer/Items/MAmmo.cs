using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Multiplayer
{
    public class MAmmo : Ammo
    {
        public void Init(UnityAction<Transform, ItemType> onDestroy, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            _onDestroy = onDestroy;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<MPlayerInventory>();
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