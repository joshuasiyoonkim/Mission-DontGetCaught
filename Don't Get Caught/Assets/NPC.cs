using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPS
{
    public class NPC : MonoBehaviour
    {
        //Outlets
        NavMeshAgent navAgent;

        //COnfiguration
        public Transform priorityTarget;
        public Transform target;
        public Transform patrolRoute;

        //State Tracking
        int patrolIndex;
        public float chaseDistance;

        //Methods
        void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (patrolRoute)
            {
                target = patrolRoute.GetChild(patrolIndex);

                float distance = Vector3.Distance(transform.position, target.position);
                print("Distance: " + distance);

                if (distance <= 5f)
                {
                    patrolIndex++;
                    if(patrolIndex >= patrolRoute.childCount)
                    {
                        patrolIndex = 0;
                    }
                }
            }

            if (priorityTarget)
            {
                float priorityTargetDistance = Vector3.Distance(transform.position, priorityTarget.position);

                if(priorityTargetDistance <= chaseDistance)
                {
                    target = priorityTarget;
                    GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    GetComponent<Renderer>().material.color = Color.white;
                }
            }

            if (target)
            {
                navAgent.SetDestination(target.position);
            }
        }
    }

}