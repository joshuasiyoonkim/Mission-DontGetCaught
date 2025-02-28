using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform player;  // Assign the player in the Inspector
    public float detectionRadius = 10f;  // Radius in which NPC detects the player
    public float chaseRadius = 15f;  // How far the NPC continues chasing
    public float chaseSpeed = 4f;  // Speed when chasing the player
    public float wanderSpeed = 2f;  // Speed when wandering
    public float wanderRadius = 10f;  // Area in which the NPC wanders

    private NavMeshAgent agent;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Wander());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            StartChase();
        }
        else if (isChasing && distanceToPlayer > chaseRadius)
        {
            StopChase();
        }

        if (isChasing)
        {
            agent.SetDestination(player.position);
        }
    }

    IEnumerator Wander()
    {
        while (!isChasing)
        {
            Vector3 randomPosition = GetRandomWanderPosition();
            agent.speed = wanderSpeed;
            agent.SetDestination(randomPosition);
            yield return new WaitForSeconds(Random.Range(3f, 6f)); // Randomize wander duration
        }
    }

    void StartChase()
    {
        if (!isChasing)
        {
            isChasing = true;
            agent.speed = chaseSpeed;
            StopCoroutine(Wander()); // Stop wandering and chase player
            Debug.Log("NPC started chasing the player!");
        }
    }

    void StopChase()
    {
        if (isChasing)
        {
            isChasing = false;
            agent.speed = wanderSpeed;
            StartCoroutine(Wander());
            Debug.Log("NPC lost the player. Resuming wander mode.");
        }
    }

    Vector3 GetRandomWanderPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, NavMesh.AllAreas);
        return navHit.position;
    }
}
