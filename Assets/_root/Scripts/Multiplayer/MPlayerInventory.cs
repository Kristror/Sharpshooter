using Photon.Pun;
using Sound;
using UnityEngine;

namespace Multiplayer
{
    public class MPlayerInventory : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] internal int _hpMax = 100;
        [SerializeField] internal int _ammoMax = 50;        
        [SerializeField] private int _score;

        public static GameObject LocalPlayerInstance;

        [SerializeField] internal int _hp;
        [SerializeField] internal int _ammo;
        private bool alive;

        private Dead dead;
        private MPauseMenu pause;
        private SoundController _soundController;
        private Renderer _renderer;
        private Collider _collider;
        private Rigidbody _rb;

        private string _killer;

        MPlayerStatus mPlayerStatus;

        public void Awake()
        {
            _killer = "";
            _soundController = FindObjectOfType<SoundController>();
            mPlayerStatus = FindObjectOfType<MPlayerStatus>();            

            _renderer = GetComponentInChildren<Renderer>();
            _collider = GetComponent<Collider>();
            _rb = GetComponent<Rigidbody>();

            _hp = _hpMax;
            _ammo = 15;
            _score  = 0;
            alive = true;

            mPlayerStatus.SetUpMaxAmmo(_ammoMax);
            mPlayerStatus.UpdateAmmo(_ammo);
            mPlayerStatus.UpdateHealth(_hp);
            mPlayerStatus.UpdateScore(_score);
            if (photonView.IsMine)
            {
               LocalPlayerInstance = gameObject;
            }
        }

        public void SetDead(Dead d)
        {
            dead = d;           
        }

        public void SetPause(MPauseMenu p)
        {
            pause = p;           
        }

        public void Revive()
        {
            _hp = _hpMax;
            _ammo = 15;

            alive = true;
            _renderer.enabled = true;
            _collider.enabled = true;
            _rb.isKinematic = false; 
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<MPlayerControlls>().isPause = false;
            GetComponent<MShooting>().isPause = false;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_hp);
                stream.SendNext(alive);
                stream.SendNext(gameObject.name);
                stream.SendNext(_killer);
            }
            else
            {
                _hp = (int)stream.ReceiveNext();
                alive = (bool)stream.ReceiveNext();
                gameObject.name = (string)stream.ReceiveNext();
                _killer = (string)stream.ReceiveNext();
            }
            if (_hp <= 0)
            {
                _hp = 0;
                Death();
            }
            mPlayerStatus.UpdateHealth(_hp);
            _renderer.enabled = alive;
            _collider.enabled = alive;
            _rb.isKinematic = !alive;
        }
        public int Ammo => _ammo;

        public int MaxAmmo => _ammoMax;

        public void PickAmmo(int pick)
        {
            _ammo += pick;
            if (_ammo > _ammoMax) _ammo = _ammoMax;
            mPlayerStatus.UpdateAmmo(_ammo);
        }
        public void AmmoDec()
        {
            _ammo--;
            mPlayerStatus.UpdateAmmo(_ammo);
        }
 
        public int Hp => _hp;

        public int MaxHp => _hpMax;

        public void PickHp(int pick)
        {
            _hp += pick;
            if (_hp > _hpMax) _hp = _hpMax;

            mPlayerStatus.UpdateHealth(_hp);
        }

        public void TakeDamage(int damage, string killer)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            _hp -= damage;
            _killer = killer;
            _soundController.Hit();

            //if(!photonView.IsMine)
            //{
            //    object[] list = new object[4] { _hp, alive, gameObject.name, killer };

            //    PhotonStream PS = new PhotonStream(true, list);
            //    PhotonMessageInfo PI = new PhotonMessageInfo();
            //    OnPhotonSerializeView(PS, PI);
            //}

            if (photonView.IsMine)
            {
                mPlayerStatus.UpdateHealth(_hp);
            }
        }

        public int Score => _score;

        public void ScorePints()
        {
            _score += 3;
        }

        public void Death()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GetComponent<MPlayerControlls>().isPause = true;
            GetComponent<MShooting>().isPause = true;

            if (pause.gameObject.activeSelf) 
                pause.Resume();
            dead.gameObject.SetActive(true);

            if (_score > 0) _score--;
            alive = false;
            _renderer.enabled = false;
            _collider.enabled = false;
            _rb.isKinematic = true;

            MPlayerInventory[] players = FindObjectsOfType<MPlayerInventory>();
            foreach (var player in players)
            {
                if (player.gameObject.name == _killer)
                {
                    player.ScorePints();
                    break;
                }
            }

        }
    }
}