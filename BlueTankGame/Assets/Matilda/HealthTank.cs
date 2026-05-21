using Unity.VisualScripting;
using UnityEngine;

public class HealthTank : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
    [SerializeField] int healAmount = 1;

    HealthScript healthScript;
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Debug.Log("Bullet collide");
            healthScript.TakeDamage(damageAmount);
        }
        if(other.CompareTag("Pickup"))
        {
            Debug.Log("Pickup collide");
            healthScript.Heal(healAmount);
        }
    }
}
