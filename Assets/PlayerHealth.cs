using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float m_CurrentHealth;
    public float m_CurrentShield;
    public float m_MaxHealth;
    public float m_MaxShield;

    public static Action OnHealthChange;
    void TakeDamage(float _amountDamage)
    {
        if(m_CurrentHealth > 0.0f)
        {
            if (m_CurrentShield > 0.0f)
            {
                m_CurrentHealth -= _amountDamage * 0.25f;
                m_CurrentShield -= _amountDamage * 0.75f;
            }

            m_CurrentHealth -= _amountDamage;
        }
        


    }
    
    public void AddHealth(int _amount)
    {
        m_CurrentHealth += _amount;
    }
}
