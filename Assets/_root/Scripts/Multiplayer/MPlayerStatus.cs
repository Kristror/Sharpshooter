using TMPro;
using UI;
using UnityEngine;

namespace Multiplayer
{
    public class MPlayerStatus : MonoBehaviour
    {
        [SerializeField] public TMP_Text _health;
        [SerializeField] public TMP_Text _score;

        public void UpdateScore(int score)
        {
            _score.text = score.ToString();
        }
        public void UpdateHealth(int health)
        {
            _health.text = health.ToString();
        }
    }
}