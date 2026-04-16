using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankShoot : MonoBehaviour
{
    private InputAction ShootAction;
    private InputAction LookAction;
    RaycastHit hitInfo;
    Vector3 lookDirection;


    void Start()
    {
        ShootAction = InputSystem.actions.FindAction("Shoot");
        LookAction = InputSystem.actions.FindAction("Look");
    }

    void Update()
    {
        lookDirection = LookAction.ReadValue<Vector2>();
        //if (ShootAction.WasPerformedThisFrame())
       // {
          //  Physics.Raycast(transform.position, transform.forward, out hitInfo, 1f);

       // }
    }
}
