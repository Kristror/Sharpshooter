using TMPro;
using UI;
using UnityEngine;

namespace Multiplayer
{
    public class MPlayerStatus : PlayerStatus
    {
        [SerializeField] public TMP_Text _score;

        public void UpdateScore(int score)
        {
            _score.text = score.ToString();
        }
    }
}