using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    Ammo, Health, Shield
}
public class ItemCollect : MonoBehaviour
{
    public Items m_MyItemType;

    public int m_AmmoAdded;
    public int m_HealthAdded;
    public int m_ShieldAdded;



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
        if(_type == Items.Shield)
            _playerCol.GetComponent<PlayerHealth>().AddShield(m_ShieldAdded);

        Destroy(gameObject);
    }


   

}
