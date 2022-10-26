using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevelInside : MonoBehaviour
{
    public Camera m_CinematicCamera;
    public Animation m_MyAnimation;
    public AnimationClip m_StairsAnimation;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCinematic();
        }
    }
    public void StartCinematic()
    {
        StartCoroutine(Cinematic());
    }

    IEnumerator Cinematic()
    {
        m_CinematicCamera.gameObject.SetActive(true);
        SetAnimation();
        yield return new WaitForSeconds(4f);
        m_CinematicCamera.gameObject.SetActive(false);
    }

    void SetAnimation()
    {
        m_MyAnimation.CrossFade(m_StairsAnimation.name, 0.1f);
    }
}
