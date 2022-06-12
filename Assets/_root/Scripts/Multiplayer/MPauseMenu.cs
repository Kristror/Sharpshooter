using Photon.Pun.Demo.PunBasics;
using Sound;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Multiplayer
{

    public class MPauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _resume;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _mainMenu;
        [SerializeField] private Button _exit;

        private SoundController _soundController;

        private GameObject _settingsMenu;

        private void Start()
        {
            _soundController = FindObjectOfType<SoundController>();

            _resume.onClick.AddListener(PlaySound);
            _resume.onClick.AddListener(Resume);
            _settings.onClick.AddListener(PlaySound);
            _settings.onClick.AddListener(Settings);
            _mainMenu.onClick.AddListener(PlaySound);
            _mainMenu.onClick.AddListener(MainMenu);
            _exit.onClick.AddListener(PlaySound);
            _exit.onClick.AddListener(Exit);
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _settingsMenu = GameObject.Instantiate((GameObject)Resources.Load("UI/SettingsMenu"), gameObject.transform.parent);

            gameObject.SetActive(false);
        }

        public void Resume()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            MPlayerInventory.LocalPlayerInstance.GetComponent<MPlayerControlls>().isPause = false;
            MPlayerInventory.LocalPlayerInstance.GetComponent<MPlayerInventory>().isPause = false;

            _settingsMenu.SetActive(false);
            gameObject.SetActive(false);
        }

        private void Settings()
        {
            _settingsMenu.GetComponent<SettingsMenu>().SetBackButton(() => { gameObject.SetActive(true); });
            _settingsMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void PlaySound()
        {
            _soundController.Button();
        }

        private void OnDestroy()
        {
            _resume.onClick.RemoveAllListeners();
            _settings.onClick.RemoveAllListeners();
            _mainMenu.onClick.RemoveAllListeners();
            _exit.onClick.RemoveAllListeners();
        }
    }
}