using Photon.Pun;
using Sound;
using UnityEngine;

namespace Multiplayer
{
    public class MPlayerInventory : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] internal int _hpMax = 100;       
        [SerializeField] private int _score;
        [SerializeField] private GameObject _bullet;

        public static GameObject LocalPlayerInstance;

        [SerializeField] internal int _hp;
        private bool alive;
        private bool isFiring;

        private Dead dead;
        private MPauseMenu pause;
        private SoundController _soundController;
        private Renderer _renderer;
        private Collider _collider;
        private Rigidbody _rb;

        private string _killer;

        [HideInInspector] public bool isPause;

        MPlayerStatus mPlayerStatus;

        public void Awake()
        {
            gameObject.name = PlayerPrefs.GetString("PlayerName");
            _killer = ""; 
            _bullet.SetActive(false);

            _soundController = FindObjectOfType<SoundController>();
            mPlayerStatus = FindObjectOfType<MPlayerStatus>();            

            _renderer = GetComponentInChildren<Renderer>();
            _collider = GetComponent<Collider>();
            _rb = GetComponent<Rigidbody>();

            _hp = _hpMax;
            _score  = 0;
            alive = true;
            mPlayerStatus.UpdateHealth(_hp);
            mPlayerStatus.UpdateScore(_score);

            if (photonView.IsMine)
            {
               LocalPlayerInstance = gameObject;
            }
        }
        public int Score => _score;

        public void ScorePints()
        {
            _score += 3;
            if (photonView.IsMine)
            {
                mPlayerStatus.UpdateScore(_score);
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

            alive = true;
            _renderer.enabled = true;
            _collider.enabled = true;
            _rb.isKinematic = false; 
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<MPlayerControlls>().isPause = false;
            isPause = false;
        }
       
        private void Update()
        {
            if (photonView.IsMine)
            {
                if (!isPause)
                {
                    if (Input.GetMouseButton(0))
                    {
                        isFiring = true;
                        _soundController.Shoot();
                    }
                    else isFiring = false;

                    _bullet.SetActive(isFiring);
                }
            }
        }
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_hp);
                stream.SendNext(_score);
                stream.SendNext(alive);
                stream.SendNext(isFiring);
                stream.SendNext(gameObject.name);
                stream.SendNext(_killer);
            }
            else
            {
                _hp = (int)stream.ReceiveNext();
                _score = (int)stream.ReceiveNext();
                alive = (bool)stream.ReceiveNext();
                isFiring = (bool)stream.ReceiveNext();
                gameObject.name = (string)stream.ReceiveNext();
                _killer = (string)stream.ReceiveNext();
            }
            if (_hp <= 0 && alive)
            {
                _hp = 0;
                Death();
            }

            _bullet.GetComponent<MBullet>().SetOwner(gameObject.name);
            _bullet.SetActive(isFiring);
            mPlayerStatus.UpdateHealth(_hp);

            _renderer.enabled = alive;
            _collider.enabled = alive;
            _rb.isKinematic = !alive;
        }
 
        public int Hp => _hp;

        public int MaxHp => _hpMax;

        public void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.CompareTag("Bullet"))
            {
                return;
            }

            MBullet bullet = other.GetComponent<MBullet>();

            _hp -= bullet.damage;
            _killer = bullet._owner;
            _soundController.Hit();
            if (photonView.IsMine)
            {
                if (_hp <= 0 && alive)
                {
                    _hp = 0;
                    Death();
                }
                mPlayerStatus.UpdateHealth(_hp);
            }
        }

        public void Death()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GetComponent<MPlayerControlls>().isPause = true;
            isPause = true;

            if (pause.gameObject.activeSelf)
                pause.Resume();
            dead.gameObject.SetActive(true);

            if (_score > 0) _score--;
            if (photonView.IsMine)
            {
                mPlayerStatus.UpdateScore(_score);
            }

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