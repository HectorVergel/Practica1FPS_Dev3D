using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IHealth
{
    void TakeDamage(float amount);
}
public class PlayerHealth : MonoBehaviour, IHealth
{
    public float m_CurrentHealth;
    public float m_CurrentShield;
    public float m_MaxHealth;
    public float m_MaxShield;

    public static Action<float> OnHealthChange;
    public static Action<float> OnShieldChange;

    public Image m_DamageEffect;
    public float m_AlphaSpeed;

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_CurrentShield = m_MaxShield;
    }
    public void TakeDamage(float _amountDamage)
    {
        if(m_CurrentHealth > 0.0f)
        {
            StartCoroutine(FadeIn());
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
        if(m_CurrentHealth <= 0.0f)
        {
            OnDie();
        }
        


    }

    public void OnDie()
    {
        m_CurrentHealth = 0.0f;
        GameController.GetGameController().RestartGame();
    }
    
    IEnumerator FadeIn()
    {
        float l_CurrentAlpha = 0.0f;
        while (m_DamageEffect.color.a <= 0.5f)
        {
            l_CurrentAlpha += m_AlphaSpeed * Time.deltaTime;
            m_DamageEffect.color = new Color(m_DamageEffect.color.r, m_DamageEffect.color.g, m_DamageEffect.color.b, l_CurrentAlpha);
             yield return null;
        }
        StartCoroutine(FadeOut());
        
    }

    IEnumerator FadeOut()
    {
        
        float l_CurrentAlpha = 0.5f;
        while (m_DamageEffect.color.a > 0.0f)
        {
            l_CurrentAlpha -= m_AlphaSpeed * Time.deltaTime;
            m_DamageEffect.color = new Color(m_DamageEffect.color.r, m_DamageEffect.color.g, m_DamageEffect.color.b, l_CurrentAlpha);
            yield return null;
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
