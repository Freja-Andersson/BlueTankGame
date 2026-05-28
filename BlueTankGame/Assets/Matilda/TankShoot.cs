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
    [SerializeField] Transform shootPoint;

    Vector2 lookInput;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (gameManager.currentState != GameManager.GameState.Playing) 
        { return; }
        HandleLooking();
    }

    public void OnLooking(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    void HandleLooking()
    {
        Vector3 lookDirection = new Vector3(lookInput.x, 0f, lookInput.y);

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * Sensetivity);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (gameManager.currentState != GameManager.GameState.Playing) { return; }
        if (context.performed && ShootDelay == false)
        {
            print("shoot");
            anim.SetTrigger("attack");
            fxPool.SpawnFX();
            Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
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
