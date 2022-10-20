using System.Collections.Generic;
using UnityEngine;

public class TCObjectPool
{
    List<GameObject> m_ObjectsPool;
    int m_CurrentID = 0;
    public TCObjectPool(int ElementCount, GameObject Element)
    {
        m_ObjectsPool = new List<GameObject>();

        for (int i = 0; i < ElementCount; i++)
        {
            GameObject l_Element = GameObject.Instantiate(Element);
            l_Element.SetActive(false);
            m_ObjectsPool.Add(l_Element);
        }
        m_CurrentID = 0;
    }

    public GameObject GetNextElement()
    {
        GameObject l_Element = m_ObjectsPool[m_CurrentID];
        ++m_CurrentID;
        if(m_CurrentID >= m_ObjectsPool.Count)
        {
            m_CurrentID = 0;
        }
        return l_Element;
    }
}



