using UnityEngine;

public class ShootingElement : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Bullet")
        {
            Debug.Log("in");
            gameObject.SetActive(false);
        }
    }
}
