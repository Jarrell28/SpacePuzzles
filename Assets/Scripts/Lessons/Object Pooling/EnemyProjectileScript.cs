using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    private string ownershipTag = "";
    private float damage = 10f;
    public float projectileSpeed = 10f;

    void Update()
    {
        transform.position += transform.forward * projectileSpeed  * Time.deltaTime;
    }

    public void Initialize(string _ownershipTag)
    {
        ownershipTag = _ownershipTag;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != null && !collision.gameObject.CompareTag(ownershipTag)  && !collision.gameObject.CompareTag("Bullet"))
        {
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }  


            PooledObject pooledObject = GetComponent<PooledObject>();
            if(pooledObject != null)
            {   
                pooledObject.ResetObject();
            } else
            {
                Destroy(this.gameObject);
            }          
        }

        
    }
}