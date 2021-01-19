using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct WayPoint
{
    [Tooltip("Way-point object to move to")]
    public GameObject target;

    [Tooltip("Dwell time to wait at object")]
    public float dwellTime;
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class WalkWayPoints : MonoBehaviour
{
    [Tooltip("List of way-points")]
    public WayPoint[] wayPoints;

    private NavMeshAgent _agent;

    private Animator _animator;

    private int _nextWayPoint;

    private bool _walking;

    private float _dwellTime;

    private float _currentTime;



    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Skip if no way-points
        if (wayPoints == null || wayPoints.Length == 0)
            return;

        // Manage end of walk
        if (_walking)
        {
            // Still walking
            if (_agent.hasPath || _agent.pathPending)
                return;

            // Clear walking animator parameter
            _walking = false;
            _animator.SetBool("isWalking", false);
        }

        // Manage dwell time and skip if dwelling
        _currentTime += Time.deltaTime;
        if (_currentTime < _dwellTime)
            return;

        // Start walking to the next way-point
        _walking = true;
        _animator.SetBool("isWalking", true);
        _agent.SetDestination(wayPoints[_nextWayPoint].target.transform.position);

        // Set the dwell for the next way-point
        _dwellTime = wayPoints[_nextWayPoint].dwellTime;
        _currentTime = 0;

        // Advance the next way-point
        _nextWayPoint = (_nextWayPoint + 1) % wayPoints.Length;
    }
}
