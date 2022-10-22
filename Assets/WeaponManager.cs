using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> m_Weapons = new List<Weapon>();

    public void SwapWeapon()
    {
        
        foreach (var weapon in m_Weapons)
        {
            if(weapon.gameObject.activeInHierarchy)
            {
                weapon.gameObject.SetActive(false);
            }
            else
            {
                weapon.gameObject.SetActive(true);
                GameController.GetGameController().GetPlayer().GetComponent<FPSPlayerController>().m_CurrentWeapon = weapon;
                GameController.GetGameController().GetInterface().UpdateBulletInterface(weapon.m_CurrentBullets, weapon.m_MaxAmountBullets);
                GameController.GetGameController().GetInterface().UpdateWeaponImage();
            }
        }

        
    }
}
