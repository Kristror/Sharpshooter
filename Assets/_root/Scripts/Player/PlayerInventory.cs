using Multiplayer;
using Sound;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Players
{
    public class PlayerInventory : MonoBehaviour, IEntity
    {
        [SerializeField] internal int _hpMax = 100;
        [SerializeField] internal int _ammoMax = 50;

        internal int _hp;
        internal int _ammo;

        private PlayerStatus _playerStatus;
        internal UnityAction _deathAction;

        private SoundController _soundController;

        public void Start()
        {            
            _hp = _hpMax;
            _ammo = 15;

            _playerStatus.UpdateAmmo(_ammo);

            _playerStatus.UpdateHealth(_hp);

            _soundController = FindObjectOfType<SoundController>();
        }
        public void Init(PlayerStatus playerStatus, UnityAction deathAction)
        {
            _playerStatus = playerStatus;
            _deathAction = deathAction;

            _playerStatus.SetUpMaxAmmo(_ammoMax);
        }

        #region Ammo
        public int Ammo => _ammo;

        public int MaxAmmo => _ammoMax;

        public void PickAmmo(int pick)
        {
            _ammo += pick;
            if (_ammo > _ammoMax) _ammo = _ammoMax;
            _playerStatus.UpdateAmmo(_ammo);
        }
        public void AmmoDec()
        {
            _ammo--;
            _playerStatus.UpdateAmmo(_ammo);
        }
        #endregion

        #region hp
        public int Hp => _hp;

        public int MaxHp => _hpMax;

        public void PickHp(int pick)
        {
            _hp += pick;
            if (_hp > _hpMax) _hp = _hpMax;

            _playerStatus.UpdateHealth(_hp);
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;

            if (_hp <= 0)
            {
                _hp = 0;

                _playerStatus.UpdateHealth(_hp);
                Death();               
            }
            else
            {
                _soundController.Hit();
            }
            _playerStatus.UpdateHealth(_hp);
        }
        public void Death()
        {
            _deathAction();
        }
        #endregion
    }
}