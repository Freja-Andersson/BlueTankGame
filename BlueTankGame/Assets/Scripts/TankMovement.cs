using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Vector3 movement;

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

    void HandleMovement()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveDirection.x, 0f, moveDirection.y);

        tankRigidbody.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
