using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProcess : MonoBehaviour
{
    public GameObject UI;

    [Header("Scout")]
    public GameObject scoutButton;
    public GameObject scoutWaitText;
    private bool scoutReady = false;

    [Header("Reader")] 
    public GameObject readerButton;
    public GameObject readerWaitText;
    private bool readerReady = false;

    private void Start()
    {
        UI.SetActive(true);
        Time.timeScale = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            scoutButton.SetActive(false);
            scoutWaitText.SetActive(true);
            scoutReady = true;
        }

        if (scoutReady && readerReady)
        {
            UI.SetActive(false);
            Destroy(this);
            Time.timeScale = 1;
        }
    }

    public void buttonInput()
    {
        readerButton.SetActive(false);
        readerWaitText.SetActive(true);
        readerReady = true;
    }
}
