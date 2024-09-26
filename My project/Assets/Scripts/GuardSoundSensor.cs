using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSoundSensor : MonoBehaviour
{
    private GuardAI guardAI;
  

    void Start()
    {
        guardAI = GetComponentInParent<GuardAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (!player.isSneaking)
            {
                guardAI.HeardSomething(other.transform.position);
            }

        }
    }
}
