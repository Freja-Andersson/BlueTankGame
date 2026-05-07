using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankShoot : MonoBehaviour
{
    [SerializeField]private InputAction ShootAction;
    private InputAction LookAction;
    bool ShootDelay = false;
    [SerializeField] float shootCooldown = 1f;
    [SerializeField] Vector3 lookDirection;
    [SerializeField] float Sensetivity = 5f;  
    [SerializeField] Animator anim;
    [SerializeField] AttackFxPool fxPool;
    [SerializeField] GameObject Bullet;



    void Start()
    {
        ShootAction = InputSystem.actions.FindAction("Attack");
        LookAction = InputSystem.actions.FindAction("Look");
    }

    void Update()
    {       
       Vector2 lookInput = LookAction.ReadValue<Vector2>();
       Vector3 lookDirection = new Vector3(lookInput.x, 0f, lookInput.y);

       if (lookDirection.sqrMagnitude > 0.01f)
       {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * Sensetivity);
       }

       if (ShootAction.WasPerformedThisFrame() && ShootDelay == false)
       {
            print("shoot");
            anim.SetTrigger("attack");
            fxPool.SpawnFX();
            Instantiate(Bullet, transform.position, transform.rotation);
            StartCoroutine(ShootCooldown());
        }

    }

    IEnumerator ShootCooldown()
    {
        ShootDelay = true;
        yield return new WaitForSeconds(shootCooldown);
        ShootDelay = false;
    }
}
