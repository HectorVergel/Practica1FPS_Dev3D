using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollider : MonoBehaviour
{
    public string m_SceneName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameController.GetGameController().GetLevel().LoadNewScene(m_SceneName);
        }
    }
}
