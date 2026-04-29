using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSensitivity = 1f;
    [SerializeField] float moveThresholdAngle = 10f; // if the moveangle is less then this, tank starts moving forward

    Vector3 movementDirection;

    InputAction moveAction;

    Rigidbody tankRigidbody;

    void Awake()
    {

        tankRigidbody = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement() // rotate the tank to face the movement direction
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        movementDirection = Vector3.right * moveInput.x + Vector3.forward * moveInput.y;

        if (movementDirection.sqrMagnitude > 0.01f)
        {   
            // Calculate  the target ratation
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSensitivity);

            float angle = Vector3.Angle(transform.forward, movementDirection); //checks the moveangle

            // 3. Om vinkeln är liten nog (vi har roterat färdigt), rör oss framåt
            if (angle < moveThresholdAngle) //If the moveangle is less, the tank starts to move forward
            {
                tankRigidbody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
            }
        }

    }

    
}
