using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
    {
        //outlets
        NavMeshAgent navAgent;
        Animator animator;

        //Configuration
        public Transform priorityTarget;
        public Transform target;
        public Transform patrolRoute;

        //State Tracking
        int patrolIndex;
        public float chaseDistance;
        private bool isChasing = false;

    // Methods
    void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

    void Update()
    {
        // Distance to priority target
        float priorityTargetDistance = priorityTarget ? Vector3.Distance(transform.position, priorityTarget.position) : Mathf.Infinity;

        // --- CHASE LOGIC ---
        if (priorityTarget && priorityTargetDistance <= chaseDistance)
        {
            isChasing = true;
            target = priorityTarget;
        }
        else if (priorityTarget && priorityTargetDistance > chaseDistance)
        {
            // Stop chasing and return to patrol
            isChasing = false;
            target = null;
        }

        // --- PATROL LOGIC ---
        if (!isChasing && patrolRoute)
        {
            if (!target) target = patrolRoute.GetChild(patrolIndex);

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= 1f)
            {
                patrolIndex = (patrolIndex + 1) % patrolRoute.childCount;
                target = patrolRoute.GetChild(patrolIndex);
            }
        }

        // --- NAVIGATION & ANIMATION ---
        if (target)
        {
            navAgent.SetDestination(target.position);
        }

        animator.SetFloat("Velocity", navAgent.velocity.magnitude);
    }
}