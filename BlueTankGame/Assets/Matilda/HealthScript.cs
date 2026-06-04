using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [SerializeField] private int damageAmount = 1;
    [SerializeField] private int healAmount = 1;
    Winner winner;
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private AudioSource healSound;

    private void Awake()
    {
        currentHealth = maxHealth;
        Health.text = "Health: " + currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        damageSound.Play();
        Health.text = "Health: " + currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        healSound.Play();
        Health.text = "Health: " + currentHealth;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            Health.text = "Health: " + currentHealth;
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");

       
        if (gameObject.layer == LayerMask.NameToLayer("TankBlue"))
        {
            Debug.Log("Blue Tank has died. Red Tank wins!");
            Winner.Instance.whowon = 1;
        }
        else if (gameObject.layer == LayerMask.NameToLayer("TankRed"))
        {
            Debug.Log("Red Tank has died. Blue Tank wins!");
            Winner.Instance.whowon = 2;
        }
        else
        {
            Debug.LogWarning("Die called on object with unexpected tag or name: " + gameObject.name);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
