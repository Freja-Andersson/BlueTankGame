using UnityEngine;

public class HealthScript : MonoBehaviour
{
        [SerializeField] private int maxHealth = 10;
        [SerializeField]  private int currentHealth;
        [SerializeField] int damageAmount = 1;
        [SerializeField] int healAmount = 1;

    void Awake()
        {
            currentHealth = maxHealth;
        }
    
        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        public void Heal(int healAmount)
        {
            currentHealth += healAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        } 

    private void Die()
    {     
            Debug.Log(gameObject.name + " has died.");
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Bullet collide");
            TakeDamage(damageAmount);
        }
        if (other.CompareTag("Pickup"))
        {
            Debug.Log("Pickup collide");
            Heal(healAmount);
        }
    }

}
