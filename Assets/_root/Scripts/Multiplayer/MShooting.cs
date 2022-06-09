using Photon.Pun;
using Sound;
using UnityEngine;

namespace Multiplayer
{
    public class MShooting : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletStartPosition;

        [HideInInspector] public bool isPause;

        private MPlayerInventory _inventory;
        private SoundController _soundController;

        public void Start()
        {
            _inventory = GetComponent<MPlayerInventory>();

            _soundController = FindObjectOfType<SoundController>();

            isPause = false;
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                if (!isPause)
                {
                    if (Input.GetMouseButtonDown(0)) Shoot();
                }
            }
        }

        public void Shoot()
        {
            if (_inventory.Ammo > 0)
            {
                var bull = PhotonNetwork.Instantiate(_bullet.name, _bulletStartPosition.position, transform.rotation);
                bull.GetComponent<MBullet>().SetOwner(gameObject.name);                
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