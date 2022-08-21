using Sound;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour, IEntity
{
    [SerializeField] public GameObject door; 
    [SerializeField] public int hp = 100;
    [SerializeField] public int damage = 15;
    [SerializeField] public float recharge = 2.5f;

    private GameObject target = null;
    private bool _isDead = false;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private bool _hit = true;
    private Vector3 _lastPos;

    private SoundController _soundController;


    public void Awake()
    {
        _soundController = FindObjectOfType<SoundController>();

        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();

        _lastPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_hit)
            {
                _animator.SetBool("Hit", true);
                collision.gameObject.GetComponent<IEntity>().TakeDamage(damage);
                _hit = false;
                Invoke("Recharge", recharge);
                Invoke("StopAnim", 0.3f);
            }
        }
    }
    public void StopAnim()
    {
        _animator.SetBool("Hit", false);
    }

    public void Recharge()
    {
        _hit = true;
    }

    public void FixedUpdate()
    {
        if (target != null)
        {
            _navMeshAgent.SetDestination(target.transform.position);
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        _soundController.SkeletonDeath();
        _isDead = true;
    }
    public void Destroy()
    {
        Destroy(door);
        Destroy(gameObject);
    }

    public void Update()
    {
        if (!_isDead)
        {
            if (transform.position != _lastPos)
            {
                _lastPos = transform.position;
                _animator.SetBool("isMove", true);
            }
            else
            {
                _animator.SetBool("isMove", false);
            }
        }
    }
}
