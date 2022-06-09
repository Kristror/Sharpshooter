using Items;
using Players;
using Sound;
using UnityEngine;

namespace SingleGame
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawn;
        [SerializeField] private Transform _itemsSpawn;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _end;

        private InputController _inputController;
        private UIController _uiController;
        private ItemController _itemController;
        private SoundController _soundController;
        private GameObject _player;

        private void Awake()
        {
            _uiController = new UIController(_canvas);

            _player = GameObject.Instantiate((GameObject)Resources.Load("Player"), _playerSpawn);

            PlayerInventory playerInventory = _player.GetComponent<PlayerInventory>();
            playerInventory.Init(_uiController.Status(),_uiController.Death);

            _end.GetComponent<EndLevel>().SetOnEnd(_uiController.Win);

            Transform bulletStart = _player.transform.GetChild(0);

            Camera.main.gameObject.transform.SetParent(_player.transform);
            Camera.main.gameObject.transform.localPosition = new Vector3(0, 1.4f, 0);

            _soundController = FindObjectOfType<SoundController>();

            _inputController = new InputController(_player, playerInventory, _soundController, bulletStart, _uiController.Pause);

            _itemController = new ItemController(_itemsSpawn);
        }

        private void Update()
        {
            _inputController.Update(); 
            _soundController.SetPosition(_player.transform.position);
        }

    }
}