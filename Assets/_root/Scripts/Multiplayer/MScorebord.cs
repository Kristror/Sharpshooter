using TMPro;
using UnityEngine;

namespace Multiplayer{

    public class MScorebord : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreboard;

        private void Start()
        {
            gameObject.SetActive(false);
            scoreboard.text = "";
        }

        public void Fill()
        {
            MPlayerInventory[] players = FindObjectsOfType<MPlayerInventory>();

            foreach (var player in players)
            {
                scoreboard.text += $"{player.gameObject.name} score:{player.Score}" + System.Environment.NewLine;
            }
        }
        public void Close()
        {
            scoreboard.text = "";
            gameObject.SetActive(false);
        }
    }
}
