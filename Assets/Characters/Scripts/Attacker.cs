using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Attacker : MonoBehaviour
{
    public GameObject target;

    public float attackDistance;

    public UnityEvent onAttack;

    private NavMeshAgent _agent;

    private Animator _animator;

    private bool _oldIsWalking;

    private bool _oldIsAttacking;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle attacking
        var distance = (target.transform.position - transform.position).magnitude;
        var isAttacking = distance < attackDistance;
        if (isAttacking != _oldIsAttacking)
        {
            _animator.SetBool("isAttacking", isAttacking);
            _oldIsAttacking = isAttacking;
        }

        // Deliver attacking event
        if (isAttacking)
            onAttack?.Invoke();

        // Handle walking
        _agent.SetDestination(target.transform.position);
        var isWalking = _agent.hasPath;
        if (isWalking != _oldIsWalking)
        {
            _animator.SetBool("isWalking", isWalking);
            _oldIsWalking = isWalking;
        }
    }
}
