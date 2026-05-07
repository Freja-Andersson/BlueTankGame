using UnityEngine;

public class HealthScript : MonoBehaviour
{
        [SerializeField] private float maxHealth = 10f;
        private float currentHealth;
    
        void Awake()
        {
            currentHealth = maxHealth;
        }
    
        public void TakeDamage(float damageAmount)
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
