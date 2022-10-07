using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    public Text m_BulletInterfaceText;
    public Text m_HealthInterfaceText;
    public Text m_ShieldInterfaceText;

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
