using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootingGallery : MonoBehaviour
{
    public TextMeshProUGUI m_CurrentScoreText;
    public GameObject m_DoorPrefab;
    int m_Score;
    public int m_MaxScore;
    

    public void AddScore(int amount)
    {
        m_Score += amount;
        RefreshHUD();
        if(m_Score >= m_MaxScore)
        {
            UnlockDoor();
        }
    }

    void RefreshHUD()
    {
        if (m_CurrentScoreText != null)
        {
            m_CurrentScoreText.text = m_Score.ToString();
        }
    }

    void UnlockDoor()
    {
        m_DoorPrefab.SetActive(false);
    }
}
