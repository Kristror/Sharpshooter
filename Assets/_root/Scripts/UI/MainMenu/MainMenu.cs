using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _quitButton;

        [SerializeField] private MultiplayerMenu _multiplayerMenu;
        [SerializeField] private SettingsMenu _settingsMenu;

        private SoundController _soundController;

        void Start()
        {
            _soundController = FindObjectOfType<SoundController>();

            _soundController.SetPosition(transform.position);

            _newGameButton.onClick.AddListener(PlaySound);
            _newGameButton.onClick.AddListener(NewGame);
            _continueButton.onClick.AddListener(PlaySound);
            _continueButton.onClick.AddListener(Continue);
            _multiplayerButton.onClick.AddListener(PlaySound);
            _multiplayerButton.onClick.AddListener(Multiplayer);
            _settingButton.onClick.AddListener(PlaySound);
            _settingButton.onClick.AddListener(Settings);
            _quitButton.onClick.AddListener(PlaySound);
            _quitButton.onClick.AddListener(Quit);

            _settingsMenu.SetBackButton(() => { gameObject.SetActive(true); });
            _multiplayerMenu.SetBackButton(() => { gameObject.SetActive(true); });

            if (!PlayerPrefs.HasKey("Level"))
            {
                _continueButton.interactable = false;
            }
        }

        private void NewGame()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Level_1");
            PlayerPrefs.SetString("Level", "Level_1");
        }
        private void Continue()
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
        }
        private void Multiplayer()
        {
            gameObject.SetActive(false);
            _multiplayerMenu.SetActive(true);
        }
        private void Settings()
        {
            gameObject.SetActive(false);
            _settingsMenu.SetActive(true);
        }
        private void Quit()
        {
            Application.Quit();
        }

        private void PlaySound()
        {
            _soundController.Button();
        }

        private void OnDestroy()
        {
            _newGameButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
            _multiplayerButton.onClick.RemoveAllListeners();
            _settingButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }
    }
}
