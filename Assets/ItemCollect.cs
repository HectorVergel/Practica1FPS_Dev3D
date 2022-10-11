using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    Ammo, Health
}
public class ItemCollect : MonoBehaviour
{
    public Items m_MyItemType;

    public int m_AmmoAdded;
    public int m_HealthAdded;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DoItemBoost(m_MyItemType, other);
        }
    }

    void DoItemBoost(Items _type, Collider _playerCol)
    {
        if (_type == Items.Ammo)
            _playerCol.GetComponent<FPSPlayerController>().AddAmmo(m_AmmoAdded);
        if (_type == Items.Health)
            _playerCol.GetComponent<PlayerHealth>().AddHealth(m_HealthAdded);

        Destroy(gameObject);
    }


   

}
