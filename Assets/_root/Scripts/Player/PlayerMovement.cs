using Sound;
using UnityEngine;

namespace Players
{
    public class PlayerMovement
    {
        private float _walk_speed = 4;
        private float _run_speed = 8;
        private float _jump_force = 11.5f;
        private float _rotationY = 120;
        private float _maxjump = 0.15f;

        private float _speed;
        private float _timeOfjump;
        private Vector3 _direction;
        private Quaternion _rotation;

        private Rigidbody _rb;
        private Animator _animator;
        private Transform _transform;

        private SoundController _soundController;

        public PlayerMovement(GameObject player, SoundController soundController)
        {
            _rb = player.GetComponent<Rigidbody>();
            _animator = player.GetComponent<Animator>();

            _transform = player.transform;

            _soundController = soundController;

            _direction = Vector3.zero;
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

        public void Move(float x,float z)
        {
            _direction.x = x;
            _direction.z = z;
            _direction = _transform.TransformDirection(_direction);          
            
            Vector3 move = (_direction * _speed * Time.fixedDeltaTime);

            if (move != Vector3.zero)
            {
                _animator.SetBool("IsMove", true);
                _soundController.Footsteps();
            }
            else
            {
                _animator.SetBool("IsMove", false);
                _soundController.StopFootsteps();
            }

            _rb.MovePosition(_transform.position + move);            
        }

        public void Rotate(float y)
        {
            _transform.Rotate(Vector3.up * y * _rotationY * Time.fixedDeltaTime);
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
    }
}