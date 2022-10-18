using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPair : MonoBehaviour
{
    public GameManager GM;

    public int room;

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
        if (other.tag == "Player")
        {
            GM.cameraActive[room] = true;
        }
    }
}
