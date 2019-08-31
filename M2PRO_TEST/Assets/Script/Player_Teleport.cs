using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            Debug.Log("左人差し指トリガーを押した");
        }
    }
}
