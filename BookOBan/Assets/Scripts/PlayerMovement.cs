using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    [Header("Controls")] 
    public KeyCode closeEyeKey = KeyCode.E;
    public KeyCode holdBreathKey = KeyCode.Q;
    public KeyCode LightToggleKey = KeyCode.F;

    public GameObject closeEyeFiter;
    public bool eyesClosed;
    public bool breathHeld;
    public bool lightOff;
    public bool moving;
    
    private Vector3 moveDirection;
    public float moveSpeed = 2;

    public float maxHealth = 30;
    public float currentHealth;
    public float healthRecovery = 3;
    public GameObject healthBar;
    public GameObject healthCloud;
    public float incomingDPS;
    public bool haunted; //Whether the player is being damaged
    
    public float maxBreath = 10;
    public float currentBreath;
    public float BreathRecovery = 1.2f;
    
    

    public GameObject sprite;

    Animator animator;
    [Header("UI Elements")] 
    public GameObject seeUI;
    public GameObject noSeeUI;
    public GameObject lightUI;
    public GameObject breathBox;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        currentBreath = maxBreath;
    }

    // Update is called once per frame
    void Update()
    {
        //Closing Eyes and holding breath
        if (Input.GetKey(closeEyeKey))
        {
            eyesClosed = true;
            closeEyeFiter.SetActive(true);
        }

        if (Input.GetKeyUp(closeEyeKey))
        {
            eyesClosed = false;
            closeEyeFiter.SetActive(false);
        }
        
        seeUI.SetActive(!eyesClosed);
        noSeeUI.SetActive(eyesClosed);
        
        if (Input.GetKey(holdBreathKey))
        {
            breathHeld = true;
        }

        if (Input.GetKeyUp(holdBreathKey))
        {
            breathHeld = false;
        }

        if (breathHeld && currentBreath > 0)
        {
            currentBreath -= Time.deltaTime;
        }
        else if (currentBreath < maxBreath)
        {
            currentBreath += Time.deltaTime;
        }

        breathBox.transform.localScale = new Vector3(0.5f, (currentBreath / maxBreath)/2, 1);
        
        
        if (Input.GetKey(LightToggleKey))
        {
            lightOff = true;
        }

        if (Input.GetKeyUp(LightToggleKey))
        {
            lightOff = false;
        }
        
        lightUI.SetActive(!lightOff);
        
        
        //Movement
        moveDirection = new Vector3(0, 1, 0) * Input.GetAxis("Vertical");
        moveDirection += new Vector3(1, 0, 0) * Input.GetAxis("Horizontal"); //These two lines get the horizontal and vertical components of movement based on player input
        moveDirection.Normalize(); //Normalize it so it's between 0 and 1
        moveDirection.z = 0; //Make sure you aren't going any z
        
        Vector3 movementVelocity = moveDirection * moveSpeed; //Multiply by speed to get velocity
        myRB.velocity = movementVelocity; //Apply that to your rigidbody

        if(moveDirection != new Vector3(0,0,0))
        {
            animator.SetBool("IsMoving", true);
            moving = true;
        }
        else
        {
            animator.SetBool("IsMoving", false);
            moving = false;
        }


        if(Input.GetAxis("Horizontal") > 0)
        {
            sprite.transform.localScale = new Vector3(-0.1f, 0.1f, 1);
            //sprite.transform.localScale = new Vector3(1, 1, -1);
        }

        if(Input.GetAxis("Horizontal") < 0)
        {
            //sprite.transform.localScale = new Vector3(1, 1, 1);
            sprite.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }

        //Health
        if (!haunted && currentHealth < maxHealth)
        {
            currentHealth += healthRecovery * Time.deltaTime;
        }
        healthBar.transform.localScale = new Vector3(295 * (currentHealth/maxHealth), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        healthCloud.transform.localScale = new Vector3(4 - (incomingDPS/2), 4 - (incomingDPS/2), 1);
        if (healthCloud.transform.localScale.x < 1.5)
        {
            healthCloud.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }


        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
