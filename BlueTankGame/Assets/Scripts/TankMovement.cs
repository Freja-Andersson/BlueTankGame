using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSensitivity = 1f;
    [SerializeField] float moveThresholdAngle = 10f; // if the moveangle is less then this, tank starts moving forward

    Vector2 moveInput;
    Vector3 movementDirection;

    Rigidbody tankRigidbody;
    GameManager gameManager;

    void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void FixedUpdate()
    {
        //if (gameManager.currentState != GameManager.GameState.Playing) { return; }
        HandleMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void HandleMovement() // rotate the tank to face the movement direction
    {
        movementDirection = Vector3.right * moveInput.x + Vector3.forward * moveInput.y;

        if (movementDirection.sqrMagnitude > 0.01f)
        {   
            // Calculate  the target ratation
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSensitivity);

            float angle = Vector3.Angle(transform.forward, movementDirection); //checks the moveangle

            if (angle < moveThresholdAngle) //If the moveangle is less, the tank starts to move forward
            {
                tankRigidbody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
            }
        }

    }

    
}
