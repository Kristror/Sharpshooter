using Sound;
using UnityEngine;

namespace Players
{
    public class Shooting
    {
        private GameObject _bullet;
        private Transform _bulletStartPosition;
        private PlayerInventory _inventory;
        private Transform _playerTransform;

        private SoundController _soundController;

        public Shooting(PlayerInventory inventory, SoundController soundController, Transform bulletStart, Transform playerTransform)
        {
            _inventory = inventory;
            _soundController = soundController;

            _bullet = (GameObject) Resources.Load("Bullet");  
            
            _bulletStartPosition = bulletStart;
            _playerTransform = playerTransform;
        }

        public void Shoot()
        {
            if (_inventory.Ammo > 0)
            {
                var bull = GameObject.Instantiate(_bullet, _bulletStartPosition.position, _playerTransform.rotation);
                _inventory.AmmoDec();

                _soundController.Shoot();
            }
            else
            {
                _soundController.Empty();
            }
        }            
    }
}