using UnityEngine;

[CreateAssetMenu(fileName = "GameControllerData", menuName = "ScriptableObjects/GameController", order = 1)]
public class GameControllerData : ScriptableObject
{
    public float m_Health;
    public float m_Shield;

    public int m_CurrentBullets;
    public int m_MaxBullets;
}
