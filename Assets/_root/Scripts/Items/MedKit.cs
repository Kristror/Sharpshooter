using Players;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class MedKit : MonoBehaviour
    {
        [SerializeField] public int amount = 35;

        internal UnityAction<Transform, ItemType> _onDestroy;

        public void Init(UnityAction<Transform,ItemType> onDestroy)
        {
            _onDestroy = onDestroy;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<PlayerInventory>();
                if (player.Hp != player.MaxHp)
                {
                    player.PickHp(amount);
                    _onDestroy(gameObject.transform.parent,ItemType.Medkit);
                    Destroy(gameObject);
                }
            }
        }        
    }
}