using Sound;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour,IEntity
{
    [SerializeField] public int hp = 100;
    [SerializeField] public int damage = 15;
    [SerializeField] public float recharge = 2.5f;
    [SerializeField] public float forgetTime = 0.5f;
    [SerializeField] public float timeforSearch = 10f;
    [SerializeField] public float searchRadius = 5f;
   

    private Quaternion _strartRot;
    internal Vector3 _startPos;

    internal GameObject target = null;
    internal NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private bool _hit = true;
    internal bool _isDead = false;
    private bool _onStartPos = true;
    private Vector3 _lastPos;

    private SoundController _soundController;

    public void Awake()
    {
        _soundController = FindObjectOfType<SoundController>();

        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();

        _startPos = transform.position;
        _strartRot = transform.rotation;
        _lastPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
            CancelInvoke("Forget");
            CancelInvoke("Search");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("Search", forgetTime);
        }
    }

    private void Search()
    {
        target = null;
        _navMeshAgent.SetDestination(new Vector3(transform.position.x + Random.Range(-searchRadius,searchRadius), transform.position.y, transform.position.z + Random.Range(-searchRadius, searchRadius)));
        Invoke("Forget", timeforSearch);
    }

    private void Forget()
    {
        _navMeshAgent.SetDestination(_startPos);
        _onStartPos = false;
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
        if (!_isDead)
        {
            if (target != null)
            {
                _navMeshAgent.SetDestination(target.transform.position);
                _onStartPos = false;
            }
            else
            {
                if ((_navMeshAgent.remainingDistance == 0) && (!_onStartPos))
                {
                    _onStartPos = true;
                    transform.rotation = _strartRot;
                }
            }
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        _soundController.SkeletonDeath();
        _animator.SetBool("isDead", true);
    }

    public void Destroy()
    {
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
