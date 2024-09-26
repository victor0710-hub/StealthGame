using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public enum GuardState { Patrolling, Investigating, Pursuing }
    public GuardState currentState;

    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    private NavMeshAgent agent;
    private Vector3 lastKnownPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = GuardState.Patrolling;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case GuardState.Patrolling:
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GoToNextPatrolPoint();
                }
                break;
            case GuardState.Investigating:
                // Move to last known position, if no player is found, return to patrol
                agent.SetDestination(lastKnownPosition);
                if (Vector3.Distance(transform.position, lastKnownPosition) < 1f)
                {
                    currentState = GuardState.Patrolling;
                    GoToNextPatrolPoint();
                }
                break;
            case GuardState.Pursuing:
                // Logic to chase the player
                break;
        }
    }

    public void HeardSomething(Vector3 soundPosition)
    {
        lastKnownPosition = soundPosition;
        currentState = GuardState.Investigating;
    }

    public void SawPlayer(Transform player)
    {
        currentState = GuardState.Pursuing;
        agent.SetDestination(player.position);
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
}
