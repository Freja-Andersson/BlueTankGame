using UnityEngine;

public class Pickup : MonoBehaviour
{
   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            Debug.Log("pickup");
            Destroy(gameObject);
        }
    }
   */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            Debug.Log("pickup");
            Destroy(gameObject);
        }
    }
}
