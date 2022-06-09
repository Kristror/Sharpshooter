using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Multiplayer
{

    public class MMedKit : MedKit
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
                if (player.Hp != player.MaxHp)
                {
                    player.PickHp(amount);
                    _onDestroy(gameObject.transform.parent, ItemType.Medkit);
                    Destroy(gameObject);
                }
            }
        }
    }
}