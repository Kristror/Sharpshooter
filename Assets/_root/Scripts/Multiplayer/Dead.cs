using Photon.Pun.Demo.PunBasics;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Multiplayer
{
    public class Dead : MonoBehaviour
    {
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Button _quitButton;

        [SerializeField] private GameManager _gameManager;

        private SoundController soundController;

        void Start()
        {
            soundController = FindObjectOfType<SoundController>();

            _respawnButton.onClick.AddListener(Play); 
            _respawnButton.onClick.AddListener(() => gameObject.SetActive(false));
            _respawnButton.onClick.AddListener(_gameManager.RespawnPlayer);

            _quitButton.onClick.AddListener(Play);
            _quitButton.onClick.AddListener(Quit);

            gameObject.SetActive(false);
        }

        void Play()
        {
            soundController.Button();
        }

        private void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OnDestroy()
        {
            _respawnButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }
    }
}