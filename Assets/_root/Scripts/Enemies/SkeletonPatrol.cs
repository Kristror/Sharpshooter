using UnityEngine;

public class SkeletonPatrol :Skeleton
{
    public Transform[] waypoints;
    private bool patrol = true;

    int _currentWaypoint;

    void Start()
    {
        _navMeshAgent.SetDestination(waypoints[0].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
            patrol = false;
            CancelInvoke("Forget");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("Forget", forgetTime);
        }
    }
    private void Forget()
    {
        target = null;
        patrol = true;
        _navMeshAgent.SetDestination(waypoints[_currentWaypoint].position);
    }

    new private void FixedUpdate()
    {
        if (target != null)
        {
            _navMeshAgent.SetDestination(target.transform.position);
        }
        else
        {       
            if ((_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)&&(patrol))
            {
                if ((_currentWaypoint + 1) == waypoints.Length) _currentWaypoint = 0;
                else _currentWaypoint++;
                _navMeshAgent.SetDestination(waypoints[_currentWaypoint].position);
            }
        }
    }
}
