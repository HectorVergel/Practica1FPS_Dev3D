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
    public Transform m_RestartButton;
    public GameObject m_RestartButtonUI;
    public float m_DistanceShowUI;
    ShootingElement[] m_MySGElements;

    private void Start()
    {
        SetSGList();
    }

    private void Update()
    {
        CheckIfCloseToButton();
    }
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

    void CheckIfCloseToButton()
    {
        if(Vector3.Distance(GameController.GetGameController().GetPlayer().transform.position, m_RestartButton.transform.position) <= m_DistanceShowUI)
        {
            m_RestartButtonUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                RestartShootingGallery();
            }
        }
        else
        {
            m_RestartButtonUI.SetActive(false);

        }
    }

    void SetSGList()
    {

        m_MySGElements = FindObjectsOfType<ShootingElement>();
    }

    void RestartShootingGallery()
    {
        foreach(ShootingElement element in m_MySGElements)
        {
            element.gameObject.SetActive(true);
        }
    }
}
