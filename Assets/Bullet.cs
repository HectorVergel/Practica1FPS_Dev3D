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
            Debug.Log("HIT");
            CreateShootHitParticles(l_raycastHit.collider, l_raycastHit.point, l_raycastHit.normal);
            Destroy(this.gameObject);
        }
           
    }

    void CreateShootHitParticles(Collider _collider, Vector3 position, Vector3 normal)
    {
        Debug.DrawRay(position, normal, Color.red, 5f);

        Instantiate(m_decalPrefab, position, Quaternion.LookRotation(normal));
    }
    public void SetBulletDirection(Vector3 _direction)
    {
        m_BulletDirection = _direction;
    }
}
