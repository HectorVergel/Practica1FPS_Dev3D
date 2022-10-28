using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        if (Physics.Raycast(transform.position, m_BulletDirection, out l_raycastHit, 1.0f, m_LayerMask.value))
        {
            if (l_raycastHit.collider.tag == "DroneCollider")
            {
                l_raycastHit.collider.GetComponent<HitCollider>().Hit();
               
            }
            if (l_raycastHit.collider.tag == "Player")
            {
                l_raycastHit.collider.GetComponent<PlayerHealth>().TakeDamage(m_BulletDamage);
            }
            if (l_raycastHit.collider.tag == "ShootingElement")
            {

                l_raycastHit.collider.GetComponent<ShootingElement>().OnBulletHit();
            }
            if (l_raycastHit.collider.tag == "Turret")
            {
                l_raycastHit.collider.GetComponent<TurretEnemy>().Hit(m_BulletDamage);

            }
            if (m_decalPrefab != null && l_raycastHit.collider.tag != "Turret" && l_raycastHit.collider.tag != "DroneCollider")
            {
                CreateShootHitParticles(l_raycastHit.collider, l_raycastHit.point, l_raycastHit.normal);

            }
            Destroy(this.gameObject);
        }

    }

    void CreateShootHitParticles(Collider _collider, Vector3 position, Vector3 normal)
    {

        GameObject l_Decal = GameController.GetGameController().GetPlayer().m_DecalPool.GetNextElement();
        l_Decal.SetActive(true);
        l_Decal.transform.position = position;
        l_Decal.transform.rotation = Quaternion.LookRotation(normal);
        l_Decal.transform.SetParent(_collider.transform);
    }
    public void SetBulletDirection(Vector3 _direction)
    {
        m_BulletDirection = _direction;
    }
}
