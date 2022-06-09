using Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button _resume;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _restart;
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
            _restart.onClick.AddListener(PlaySound);
            _restart.onClick.AddListener(Restart);
            _mainMenu.onClick.AddListener(PlaySound);
            _mainMenu.onClick.AddListener(MainMenu);
            _exit.onClick.AddListener(PlaySound);
            _exit.onClick.AddListener(Exit);

            _settingsMenu = GameObject.Instantiate((GameObject)Resources.Load("UI/SettingsMenu"), gameObject.transform.parent);


            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }       

        private void Resume()
        {
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Destroy(gameObject);
            Destroy(_settingsMenu);
        }
        
        private void Settings()
        {
            _settingsMenu.GetComponent<SettingsMenu>().SetBackButton(() => {gameObject.SetActive(true);});
            _settingsMenu.SetActive(true); 
            gameObject.SetActive(false);
        }
        
        private void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
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
            _restart.onClick.RemoveAllListeners();
            _mainMenu.onClick.RemoveAllListeners();
            _exit.onClick.RemoveAllListeners();
        }
    }
}