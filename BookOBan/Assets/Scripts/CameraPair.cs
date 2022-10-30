using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPair : MonoBehaviour
{
    public GameManager GM;

    public int room;
    public KeyCode fixKey = KeyCode.T;
    public GameObject icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.cameraActive[room])
        {
            icon.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            icon.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.GetComponent<PlayerMovement>().haunted && Input.GetKey(fixKey))
        {
            GM.cameraActive[room] = true;

        }
    }
}
