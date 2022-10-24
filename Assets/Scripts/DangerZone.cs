using UnityEngine;

public class DangerZone : MonoBehaviour
{
    


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().OnDie();
            
        }
    }
}
