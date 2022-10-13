using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public AnimationClip m_OpenDoor;
    public AnimationClip m_CloseDoor;
    public Animation m_MyDoorAnimation;

    private bool m_IsOpen;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_MyDoorAnimation.CrossFade(m_OpenDoor.name);
            m_IsOpen = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_MyDoorAnimation.CrossFade(m_CloseDoor.name);
            m_IsOpen = false;
        }
    }
}
