using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Sound;

namespace UI
{
    public class WinDeath : MonoBehaviour
    {
        [SerializeField] public Button _restart;
        [SerializeField] public Button _nextLevel;
        [SerializeField] public Button _mainMenu;

        [SerializeField] public TMP_Text _winDeath;

        private SoundController _soundController;


        public void Awake()
        {
            _soundController = FindObjectOfType<SoundController>();

            _restart.onClick.AddListener(PlaySound);
            _restart.onClick.AddListener(Restart);
            _nextLevel.onClick.AddListener(PlaySound);
            _nextLevel.onClick.AddListener(NextLevel);
            _mainMenu.onClick.AddListener(PlaySound);
            _mainMenu.onClick.AddListener(MainMenu);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Death()
        {
            _soundController.StopMusic();
            _soundController.StopFootsteps();
            _soundController.Death();

            _winDeath.text = "You are Dead!";
            _winDeath.color = Color.red;

            _nextLevel.gameObject.SetActive(false);
        }

        public void Win()
        {
            _soundController.StopMusic();
            _soundController.StopFootsteps();
            _soundController.Win();

            _winDeath.text = "You Win!";
            _winDeath.color = Color.green;

            _restart.gameObject.SetActive(false);

            if (SceneManager.GetActiveScene().name  == "Level_2")
            {                 
                _nextLevel.gameObject.SetActive(false);
            }
            else
            {
                _mainMenu.gameObject.SetActive(false);
            }
        }

        private void Restart()
        {
            Time.timeScale = 1;
            _soundController.Music();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void MainMenu()
        {
            Time.timeScale = 1;
            _soundController.Music();
            SceneManager.LoadScene("MainMenu"); 
        }

        private void NextLevel()
        {
            Time.timeScale = 1;
            _soundController.Music();
            SceneManager.LoadScene("Level_2");
            PlayerPrefs.SetString("Level", "Level_2");
        }

        private void PlaySound()
        {
            _soundController.Button();
        }

        private void OnDestroy()
        {
            _restart.onClick.RemoveAllListeners();
            _nextLevel.onClick.RemoveAllListeners();
            _mainMenu.onClick.RemoveAllListeners();
        }
    }
}