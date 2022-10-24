using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    
    void Update()
    {
        transform.LookAt(GameController.GetGameController().GetPlayer().transform.position);
    }
}
