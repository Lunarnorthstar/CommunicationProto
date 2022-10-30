using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed;
    
    
    public bool closeEyesGoal; //Whether the player should close their eyes or not to defeat this entity
    public bool caresEyes; //Whether the player's eyes matter about defeating the entity
    public bool holdBreathGoal;
    public bool caresBreath;
    public bool movingGoal;
    public bool caresMoving;
    public bool lightOffGoal;
    public bool caresLight;
    
    [Header("Anti")]
    public bool hatesCloseEyesGoal; //Whether the player should close their eyes or not to provoke this entity
    public bool hatesCaresEyes; //Whether the player's eyes matter about provoking the entity
    public bool hatesHoldBreathGoal;
    public bool hatesCaresBreath;
    public bool hatesMovingGoal;
    public bool hatesCaresMoving;
    public bool hatesLightOffGoal;
    public bool hatesCaresLight;
    public int hateExtraDamage = 1;

    public float damagePerSecond = 1;

    private GameObject player;

    private PlayerMovement PM;

    public GameManager GM;
    
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
        Vector3 moveDir = player.transform.position - gameObject.transform.position;
        moveDir.Normalize();
        moveDir.z = 0;
        Vector3 moveVeloc = moveDir * moveSpeed;
        
        gameObject.GetComponent<Rigidbody2D>().velocity = moveVeloc; //Apply that to your rigidbody
    }

    private bool tick = false;
    public float timeToDefeat;
    private float timer = 0;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PM.haunted = true;
            if (!tick)
            {
                PM.incomingDPS += damagePerSecond;
                tick = true;
            }
            
            PM.currentHealth -= damagePerSecond * Time.deltaTime;

            if ((PM.eyesClosed == hatesCloseEyesGoal || !hatesCaresEyes) && (PM.breathHeld == hatesHoldBreathGoal || !hatesCaresBreath) && (PM.lightOff == hatesLightOffGoal || !hatesCaresLight) && (PM.moving == hatesMovingGoal || !hatesCaresMoving))
            {
                PM.currentHealth -= hateExtraDamage * Time.deltaTime;
            }
            
            

            if ((PM.eyesClosed == closeEyesGoal || !caresEyes) && (PM.breathHeld == holdBreathGoal || !caresBreath) && (PM.lightOff == lightOffGoal || !caresLight) && (PM.moving == movingGoal || !caresMoving))
            {
                timer += Time.deltaTime;
                if (timer >= timeToDefeat)
                {
                    PM.haunted = false;
                    Destroy(gameObject);
                }
            }
            else
            {
                timer = 0;
            }
            
            
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PM.haunted = false;
            PM.incomingDPS -= damagePerSecond;
            tick = false;
        }
    }
}
