using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public TextMeshProUGUI m_CurrentBulletInterfaceText;
    public TextMeshProUGUI m_MaxBulletInterfaceText;
    public TextMeshProUGUI m_HealthInterfaceText;
    public TextMeshProUGUI m_ShieldInterfaceText;

    public Image m_HealthImage;
    public Image m_ShieldImage;

    private void OnEnable()
    {
        FPSPlayerController.OnReload += UpdateBulletInterface;
        FPSPlayerController.OnPickAmmo += UpdateBulletInterface;
        PlayerHealth.OnHealthChange += UpdateHealthInterface;
        PlayerHealth.OnShieldChange += UpdateShieldInterface;
    }

    private void OnDisable()
    {
        FPSPlayerController.OnReload -= UpdateBulletInterface;
        FPSPlayerController.OnPickAmmo -= UpdateBulletInterface;
        PlayerHealth.OnHealthChange -= UpdateHealthInterface;
        PlayerHealth.OnShieldChange -= UpdateShieldInterface;

    }
    public void UpdateBulletInterface(int _currentBullets, int _maxBullets)
    {
        m_CurrentBulletInterfaceText.text = _currentBullets.ToString();
        m_MaxBulletInterfaceText.text =  _maxBullets.ToString();
    }

    public void UpdateBulletInterface(int _maxBullets)
    {
        
        m_MaxBulletInterfaceText.text =  _maxBullets.ToString();
    }

    public void UpdateHealthInterface(float _currentHealth)
    {
        m_HealthInterfaceText.text = _currentHealth.ToString();
        m_HealthImage.fillAmount = _currentHealth / 100.0f;
    }

    public void UpdateShieldInterface(float _currentShield)
    {
        m_ShieldInterfaceText.text = _currentShield.ToString();
        m_ShieldImage.fillAmount = _currentShield / 100.0f;
    }
}
