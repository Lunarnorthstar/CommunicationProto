using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool closeEyesGoal;

    public bool holdBreathGoal;

    public float damagePerSecond = 1;

    private GameObject player;

    private PlayerMovement PM;

    public GameManager GM;

    public int room;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        PM = player.GetComponent<PlayerMovement>();

        GM = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PM.haunted = true;
            PM.currentHealth -= damagePerSecond * Time.deltaTime;
            
            
            if (PM.eyesClosed == closeEyesGoal && PM.breathHeld == holdBreathGoal)
            {
                PM.haunted = false;
                GM.cameraHaunted[room] = false;
                Destroy(gameObject);
            }
        }
        
    }
}
