using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    public TextMeshProUGUI m_BulletInterfaceText;
    public TextMeshProUGUI m_HealthInterfaceText;
    public TextMeshProUGUI m_ShieldInterfaceText;

    private void OnEnable()
    {
        FPSPlayerController.OnReload += UpdateBulletInterface;
    }

    private void OnDisable()
    {
        FPSPlayerController.OnReload -= UpdateBulletInterface;
    }
    public void UpdateBulletInterface(int _currentBullets)
    {
        m_BulletInterfaceText.text = _currentBullets.ToString();
    }

    public void UpdateHealthInterface(int _currentHealth)
    {

    }

    public void UpdateShieldInterface(int _currentShield)
    {

    }
}
