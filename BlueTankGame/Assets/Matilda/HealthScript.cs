using UnityEngine;

public class HealthScript : MonoBehaviour
{
        [SerializeField] private int maxHealth = 10;
        private int currentHealth;
    
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
    
        private void Die()
        {     
            Debug.Log(gameObject.name + " has died.");
            Destroy(gameObject);
        }

   
}
