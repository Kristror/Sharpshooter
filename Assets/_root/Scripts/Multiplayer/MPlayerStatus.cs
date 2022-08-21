using TMPro;
using UI;
using UnityEngine;

namespace Multiplayer
{
    public class MPlayerStatus : MonoBehaviour
    {
        [SerializeField] public TMP_Text _health;
        public void UpdateHealth(int health)
        {
            _health.text = health.ToString();
        }
    }
}