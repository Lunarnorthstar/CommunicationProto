using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;


public class BanishLogic : MonoBehaviour
{
    [SerializeField] private bool canInteract = false;
    [SerializeField] private KeyCode interactKey = KeyCode.Space;
    public GameObject deletedSymbol;

    public SymbolManager SM;
    public GameObject UI;

    public int successes = 0;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(interactKey))
            {
                UI.SetActive(!UI.activeSelf);
            }
        }
        else
        {
            UI.SetActive(false);
        }

        if (UI.activeSelf)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ButtonInput(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ButtonInput(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ButtonInput(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ButtonInput(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ButtonInput(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                ButtonInput(5);
            }
        }

        if (successes == 3)
        {
            SM.SendMessage("GameWin");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canInteract = false;
        }
    }

    public void ButtonInput(int input)
    {
        for (int i = 0; i < 3; i++)
        {
            if (input == SM.chosenSymbols[i])
            {
                SM.chosenSymbols[i] = -1; //"Null" it
                SM.activeSymbols[i] = deletedSymbol;
                successes++;
                return;
            }
        }
        Debug.Log("Not right! In the future, that will be bad.");
    }
}
