using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BulletPrefab : MonoBehaviour
{
    [SerializeField] private float speed = 0.0000000005f;
    [SerializeField] private float time = 2f;

    private void Start()
    {
        Quaternion rotation = Quaternion.LookRotation(transform.forward);
    }
    void Update()
    {
        transform.position += transform.forward * speed;
        StartCoroutine(destroyCoroutine());
    }
 
    IEnumerator destroyCoroutine()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            Debug.Log("Bullet collide");
            Destroy(gameObject);
        }
    }
   */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            Debug.Log("Bullet collide");
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Bullet collide");
            Destroy(gameObject);
        }
    }

}
