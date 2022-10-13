using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public float m_Damage;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(m_Damage);
            
        }
    }
}
