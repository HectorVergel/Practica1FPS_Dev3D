using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public AnimationClip m_OpenDoor;
    public AnimationClip m_CloseDoor;
    public Animation m_MyDoorAnimation;
    public Transform m_Checkpoint;
    public bool m_RequireKey;

    private bool m_IsOpen;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !m_RequireKey)
        {
            m_MyDoorAnimation.CrossFade(m_OpenDoor.name);
            m_IsOpen = true;
            GameController.GetGameController().GetLevel().SetNewCheckPoint(m_Checkpoint);
        }

        if (other.tag == "Player" && m_RequireKey)
        {
            if (GameController.GetGameController().GetPlayer().m_HaveKey)
            {
                m_MyDoorAnimation.CrossFade(m_OpenDoor.name);
                m_IsOpen = true;
                m_RequireKey = false;
                GameController.GetGameController().GetPlayer().m_HaveKey = false;
                GameController.GetGameController().GetLevel().SetNewCheckPoint(m_Checkpoint);
            }
           
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && m_IsOpen)
        {
            m_MyDoorAnimation.CrossFade(m_CloseDoor.name);
            m_IsOpen = false;
        }
    }
}
