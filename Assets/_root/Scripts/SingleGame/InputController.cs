using Players;
using Sound;
using UnityEngine;
using UnityEngine.Events;

namespace SingleGame
{
    public class InputController
    {
        private PlayerMovement _playerMovement;
        private Shooting _shooting;

        private UnityAction Pause;

        public InputController(GameObject player,PlayerInventory playerInventory, SoundController soundController, Transform bulletStart, UnityAction pause)
        {
            _playerMovement = new PlayerMovement(player, soundController);            
            _shooting = new Shooting(playerInventory, soundController, bulletStart, player.transform);

            Pause = pause;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        public void Update()
        {
            if (Time.timeScale == 1)
            {
                if (Input.GetButtonDown("Cancel")) Pause();
                if (Input.GetButton("Run"))
                {
                    _playerMovement.Run();
                }
                else
                {
                    _playerMovement.Walk();
                }

                _playerMovement.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                
                _playerMovement.Jump(Input.GetAxis("Jump"));

                if (Input.GetAxis("Mouse X") != 0) _playerMovement.Rotate(Input.GetAxis("Mouse X"));

                if (Input.GetMouseButtonDown(0)) _shooting.Shoot();
            }
        }
    }
}