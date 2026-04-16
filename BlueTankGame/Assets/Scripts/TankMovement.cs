using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSensitivity = 1f;
    [SerializeField] bool inRotation;

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

        if(movementDirection.sqrMagnitude > 0.01f) // only rotates if there is movement 
        {
            inRotation = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), Time.fixedDeltaTime * rotateSensitivity);
        }
        else
        {
            inRotation = false;
        }

        //fix so the tank only rotates, and not moves until the right direction and than move forward

        if(inRotation) // only rotate to change the direction
        {
            tankRigidbody.MovePosition(transform.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
        if (!inRotation) // move the tank forward in the direction
        {
            tankRigidbody.MovePosition(transform.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
        
    }

    
}
