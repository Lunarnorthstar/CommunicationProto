using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHaunting : MonoBehaviour
{
    public GameManager gm;

    public int cameraNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            gm.SendMessage("HauntCamera", cameraNumber);
        }
    }
}
