using Unity.VisualScripting;
using UnityEngine;

public class HealthTank : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;

    HealthScript healthScript;
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
    }

    void Update()
    {
       
    }
    void OnCollisionEnter(Collision collision)
    { 
      if (collision.gameObject.CompareTag("Bullet"))
      {
          healthScript.TakeDamage(damageAmount);
      }
    }
}
