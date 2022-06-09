using UI;
using UnityEngine;

namespace SingleGame
{
    public class UIController
    {
        private GameObject _canvas;

        private GameObject _pauseMenu;
        private GameObject _playerStatus;
        private GameObject _winDeath;

        public UIController(GameObject canvas)
        {
            _canvas = canvas;

            _pauseMenu = (GameObject) Resources.Load("UI/PauseMenu");
            _playerStatus = (GameObject) Resources.Load("UI/PlayerStatus");
            _winDeath = (GameObject) Resources.Load("UI/WinDeath");
        }

        public void Pause()
        {
            Time.timeScale = 0;
            GameObject.Instantiate(_pauseMenu, _canvas.transform); 
        }

        public PlayerStatus Status()
        {
           return GameObject.Instantiate(_playerStatus, _canvas.transform).GetComponent<PlayerStatus>();
        }       
        
        public void Win()
        {
            Time.timeScale = 0;
            GameObject.Instantiate(_winDeath, _canvas.transform).GetComponent<WinDeath>().Win();
        }

        public void Death()
        {
            Time.timeScale = 0;
            GameObject.Instantiate(_winDeath, _canvas.transform).GetComponent<WinDeath>().Death();
        }
    }
}