using UnityEngine;

public class GuardSightSensor : MonoBehaviour
{
    private GuardAI guardAI;
    public float detectionAngle = 45f;
    public LayerMask obstacleMask;

    void Start()
    {
        guardAI = GetComponentInParent<GuardAI>();
        if (guardAI == null)
        {
            Debug.LogError("GuardAI component is missing on " + transform.parent.name);
            this.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null)
            {
                Debug.LogError("PlayerController component not found on player object. Make sure it's attached and the object is correctly tagged as 'Player'.");
                return;
            }

            Vector3 directionToPlayer = other.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            directionToPlayer.Normalize();

            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer, obstacleMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Sound detection logic
                    if (!player.IsSneaking)
                    {
                        guardAI.HeardSomething(other.transform.position);
                    }

                    // Sight detection logic
                    float angle = Vector3.Angle(transform.forward, directionToPlayer);
                    if (angle < detectionAngle / 2f)
                    {
                        guardAI.SawPlayer(other.transform);
                    }
                }
            }
            else
            {
                Debug.Log("Raycast did not hit the player. May be blocked by an obstacle.");
            }
        }
    }

}
