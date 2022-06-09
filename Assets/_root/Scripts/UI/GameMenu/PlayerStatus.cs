using UnityEngine;
using TMPro;

namespace UI
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] public TMP_Text _health;
        [SerializeField] public TMP_Text _ammo;
        [SerializeField] public TMP_Text _maxAmmo;

        public void UpdateHealth(int hp)
        {
            _health.text = hp.ToString();
        }        

        public void UpdateAmmo(int ammo)
        {
            _ammo.text = ammo.ToString();
        }
        public void SetUpMaxAmmo(int maxAmmo)
        {
            _maxAmmo.text = maxAmmo.ToString();
        }
    }
}