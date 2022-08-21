using Photon.Pun;
using Sound;
using UnityEngine;

namespace Multiplayer
{
    public class MPlayerControlls : MonoBehaviourPunCallbacks
    {
        private float _walk_speed = 4;
        private float _run_speed = 8;
        private float _jump_force = 12;
        private float _rotationY = 120;
        private float _maxjump = 0.15f;

        private float _speed;
        private float _timeOfjump;
        private Vector3 _direction;

        private Rigidbody _rb;
        [HideInInspector] public bool isPause;
        private Animator _animator;

        private SoundController _soundController;

        private bool _isMoving;
        private bool _isRuning;

        private Vector3 _lastPosition = Vector3.zero;

        public void Awake()
        {
            _isMoving = false;
            _isRuning = false;

               _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _soundController = FindObjectOfType<SoundController>();

            _direction = Vector3.zero;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPause = false;

            if (photonView.IsMine)
            {
                Camera.main.gameObject.transform.SetParent(transform);
                Camera.main.gameObject.transform.localPosition = new Vector3(0, 1.4f, 0);
            }
        }

        public void Run()
        {
            _speed = _run_speed;
            _animator.speed = 1.25f;
        }
        public void Walk()
        {
            _speed = _walk_speed;
            _animator.speed = 1;
        }

        public void Move(float x, float z)
        {
            _direction.x = x;
            _direction.z = z;
            _direction = transform.TransformDirection(_direction);

            Vector3 move = (_direction * _speed * Time.fixedDeltaTime);

            if (move != Vector3.zero)
            {
                _animator.SetBool("IsMove", true);
                _isMoving = true;
                _soundController.Footsteps();
            }
            else
            {
                _animator.SetBool("IsMove", false);
                _isMoving = false;
                _soundController.StopFootsteps();
            }

            _rb.MovePosition(transform.position + move);
        }

        public void Rotate(float y)
        {
            transform.Rotate(Vector3.up * y * _rotationY * Time.fixedDeltaTime);
        }

        public void Jump(float jump)
        {
            int speed = 1;

            if (jump == 0)
            {
                _timeOfjump = 0;
            }

            if ((_timeOfjump == 0) && (jump == 1))
            {
                _timeOfjump = Time.time;
            }

            if (Time.time - _timeOfjump > _maxjump)
            {
                speed = 0;
            }
            else
            {
                _rb.AddForce(Vector3.up * _jump_force * jump * speed, ForceMode.Impulse);
            }
        }
        private void Update()
        {
            if (photonView.IsMine)
            {
                if (!isPause)
                {
                    if (Input.GetButton("Run"))
                    {
                        Run();
                    }
                    else
                    {
                        Walk();
                    }

                    Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                    Jump(Input.GetAxis("Jump"));

                    if (Input.GetAxis("Mouse X") != 0) Rotate(Input.GetAxis("Mouse X"));
                }
            }
            else
            {
                if (_lastPosition != transform.position.normalized)
                    _animator.SetBool("IsMove", true);
                else
                    _animator.SetBool("IsMove", false);

                _lastPosition = transform.position.normalized;

            }
        }
    }
}