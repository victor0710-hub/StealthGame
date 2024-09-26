using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sneakSpeed = 2f;
    public bool isSneaking = false;  // Private variable to store sneaking state

    

    private CharacterController controller;

    // Public property to get the sneaking status safely
    public bool IsSneaking
    {
        get { return isSneaking; }
    }

    void Start()
    {
        // Get the CharacterController attached to this GameObject
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Handle input for movement
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get horizontal and vertical input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Create a movement vector in the horizontal and vertical directions
        Vector3 movement = new Vector3(h, 0f, v) * Time.deltaTime;

        // Check for sneaking input (e.g., holding down the shift key)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSneaking = true;
            controller.Move(movement * sneakSpeed);  // Move slower when sneaking
        }
        else
        {
            isSneaking = false;
            controller.Move(movement * moveSpeed);  // Move at normal speed
        }
    }
}
