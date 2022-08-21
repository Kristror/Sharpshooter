using Photon.Pun;
using Sound;
using UnityEngine;

namespace Multiplayer
{
    public class MPlayerInventory : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] internal int _hpMax = 100;       
        [SerializeField] private int _score = 0;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private GameObject _nameTag;

        public static GameObject LocalPlayerInstance;

        [SerializeField] internal int _hp;
        private bool alive;
        private bool isFiring;

        private MDeathMenu _mDeathMenu;
        private MPauseMenu _mPauseMenu;
        private SoundController _soundController;
        private Renderer _renderer;
        private Collider _collider;
        private Rigidbody _rb;

        private string _killer;
        private bool _scored;
        private bool _scoredDeath;

        [HideInInspector] public bool isPause;

        MPlayerStatus mPlayerStatus;

        public void Awake()
        {
            gameObject.name = PlayerPrefs.GetString("PlayerName");
            _killer = ""; 

            _bullet.SetActive(false);
            _bullet.GetComponent<MBullet>().SetOwner(gameObject.name);

            _soundController = FindObjectOfType<SoundController>();
            mPlayerStatus = FindObjectOfType<MPlayerStatus>();            

            _renderer = GetComponentInChildren<Renderer>();
            _collider = GetComponent<Collider>();
            _rb = GetComponent<Rigidbody>();

            _hp = _hpMax;
            mPlayerStatus.UpdateHealth(_hp);

            _score  = 0;
            _scored = false;
            _scoredDeath = false;
            alive = true;

            if (photonView.IsMine)
            {
               LocalPlayerInstance = gameObject;
            }
        }
        public int Score => _score;

        public void ScorePoints()
        {
            if (photonView.IsMine)
            {
                _score += 3;
            }
        }
        public void SetDeathMenu(MDeathMenu deathMenu)
        {
            _mDeathMenu = deathMenu;           
        }

        public void SetPause(MPauseMenu mPauseMenu)
        {
            _mPauseMenu = mPauseMenu;           
        }

        public void Revive()
        {
            alive = true;
            if (photonView.IsMine)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                GetComponent<MPlayerControlls>().isPause = false;
                isPause = false;
            }
            else
                _nameTag.SetActive(true);

            _hp = _hpMax;

            _renderer.enabled = true;
            _collider.enabled = true;
            _rb.isKinematic = false;


            _scored = false;
            _scoredDeath = false;
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
                stream.SendNext(_score);
                stream.SendNext(alive);
                stream.SendNext(isFiring);
                stream.SendNext(gameObject.name);
            }
            else
            {
                _score = (int)stream.ReceiveNext();
                alive = (bool)stream.ReceiveNext();
                isFiring = (bool)stream.ReceiveNext();
                gameObject.name = (string)stream.ReceiveNext(); 
            }

            if (!alive)
            {
                Death();
            }

            _bullet.SetActive(isFiring);
        }
 
        public int Hp => _hp;

        public int MaxHp => _hpMax;

        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Bullet"))
            {
                return;
            }

            MBullet bullet = other.GetComponent<MBullet>();

            if (!photonView.IsMine)
            {
                _killer = bullet._owner;
                return;
            }
            else
            {
                _hp -= bullet.damage;
                _soundController.Hit();

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
            alive = false;
            _renderer.enabled = false;
            _collider.enabled = false;
            _rb.isKinematic = true;
            _nameTag.SetActive(false);

            if (photonView.IsMine)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                GetComponent<MPlayerControlls>().isPause = true;
                isPause = true;

                if (_mPauseMenu.gameObject.activeSelf)
                    _mPauseMenu.Resume();
                _mDeathMenu.gameObject.SetActive(true);

                if (_score > 0 && !_scoredDeath)                    
                {
                    _score--;
                    _scoredDeath = true;
                }
            }
            else
            if(!_scored)
            {
                GameObject.Find(_killer).GetComponent<MPlayerInventory>().ScorePoints();
                _scored = true;
            }
        }
    }
}