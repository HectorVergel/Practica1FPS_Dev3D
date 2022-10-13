using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float m_CurrentHealth;
    private float m_CurrentShield;
    public float m_MaxHealth;
    public float m_MaxShield;

    public static Action<float> OnHealthChange;
    public static Action<float> OnShieldChange;


    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_CurrentShield = m_MaxShield;
    }
    public void TakeDamage(float _amountDamage)
    {
        if(m_CurrentHealth > 0.0f)
        {
            if (m_CurrentShield > 0.0f)
            {
                m_CurrentHealth -= _amountDamage * 0.25f;
                m_CurrentShield -= _amountDamage * 0.75f;
            }
            else
            {
                m_CurrentHealth -= _amountDamage;
            }

          
            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0.0f, m_MaxHealth);
            m_CurrentShield = Mathf.Clamp(m_CurrentShield, 0.0f, m_MaxShield);
            OnHealthChange.Invoke(m_CurrentHealth);
            OnShieldChange.Invoke(m_CurrentShield);
           
        }
        else
        {
            Destroy(gameObject);
        }
        


    }
    
    public void AddHealth(float _amount)
    {
        if (m_CurrentHealth < m_MaxHealth)
        {
            m_CurrentHealth += _amount;
            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0.0f, m_MaxHealth);
            OnHealthChange.Invoke(m_CurrentHealth);
        }
       
    }

    public void AddShield(int _amount)
    {
        if (m_CurrentShield < m_MaxShield)
        {
            
            m_CurrentShield += _amount;
            m_CurrentShield = Mathf.Clamp(m_CurrentShield, 0.0f, m_MaxShield);
            OnShieldChange.Invoke(m_CurrentShield);
        }

    }

    public float GetLife()
    {
        return m_CurrentHealth;
    }
}
