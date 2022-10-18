using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_BulletSpeed;
    public LayerMask m_LayerMask;
    public float m_BulletDamage;
    private Vector3 m_BulletDirection;
    public GameObject m_decalPrefab;

    
    void Update()
    {
        
        transform.position += m_BulletSpeed * m_BulletDirection * Time.deltaTime;

        RaycastHit l_raycastHit;

        if (Physics.Raycast(transform.position, m_BulletDirection, out l_raycastHit, 1f, m_LayerMask.value))
        {
            if(l_raycastHit.collider.tag == "DroneCollider")
            {
               
                l_raycastHit.collider.GetComponent<HitCollider>().Hit();
            }
            if(l_raycastHit.collider.tag == "Player")
            {
                l_raycastHit.collider.GetComponent<PlayerHealth>().TakeDamage(m_BulletDamage);
            }
            if(m_decalPrefab != null)
            {
                CreateShootHitParticles(l_raycastHit.collider, l_raycastHit.point, l_raycastHit.normal);

            }
            Destroy(this.gameObject);
        }
           
    }

    void CreateShootHitParticles(Collider _collider, Vector3 position, Vector3 normal)
    {

        Instantiate(m_decalPrefab, position, Quaternion.LookRotation(normal));
    }
    public void SetBulletDirection(Vector3 _direction)
    {
        m_BulletDirection = _direction;
    }
}
