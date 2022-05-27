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
        [SerializeField] private SettingsMenu _SettingsMenu;

        void Start()
        {
            _newGameButton.onClick.AddListener(NewGame);
            _continueButton.onClick.AddListener(Continue);
            _multiplayerButton.onClick.AddListener(Multiplayer);
            _settingButton.onClick.AddListener(Settings);
            _quitButton.onClick.AddListener(Quit);

            _SettingsMenu.SetBackButton(() => { gameObject.SetActive(true); });
        }

        private void NewGame()
        {
            //удаляем сейв
            SceneManager.LoadScene("Level_1");
        }
        private void Continue()
        {
            //читаем сейв грузим уровень
        }
        private void Multiplayer()
        {
            //скрывем это меню и открывем меню
        }
        private void Settings()
        {
            gameObject.SetActive(false);
            _SettingsMenu.SetActive(true);
        }
        private void Quit()
        {
            Application.Quit();
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
