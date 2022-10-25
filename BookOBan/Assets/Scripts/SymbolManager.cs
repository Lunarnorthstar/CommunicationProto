using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SymbolManager : MonoBehaviour
{
    public GameObject[] symbolPrefabs; //The corresponding prefabs

    public GameObject[] spawnPositions; //The positions symbols can spawn at
    private bool[] occupied; //Whether or not a given position is occupied
    
    public int[] chosenSymbols = new int[3]; //A list of which symbols are chosen
    public GameObject[] activeSymbols = new GameObject[3]; //The spawned symbols themselves

    public int minSymbolMoveTime = 60; //The minimum time it takes for symbols to move
    public int maxSymbolMoveTime = 240; //The maximum time it takes for symbols to move

    [SerializeField] private float timer = 1000; //Timer.
    
    // Start is called before the first frame update
    void Start()
    {
        occupied = new bool[spawnPositions.Length]; //Re-initialize the occupied array
        
        bool processing = true; //Start the while loop

        while (processing)
        {
            int a = Random.Range(0, symbolPrefabs.Length);
            int b = Random.Range(0, symbolPrefabs.Length);
            int c = Random.Range(0, symbolPrefabs.Length); //Generate three random numbers in the symbol type array

            if (a != b && a != c && b != c) //If they are all different...
            {
                chosenSymbols[0] = a;
                chosenSymbols[1] = b;
                chosenSymbols[2] = c; //Assign those numbers to the chosen symbols array. This is used later to detect proper ritual input.
                processing = false; //Stop re-generating.
            }
        }
        SymbolSpawning(); //Spawn the symbols
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; //Tick down the timer

        if (timer <= 0) //When it hits zero...
        {
            SymbolMoving(); //Move the symbols
        }
        
    }

    public void SymbolSpawning()
    {
        int symbolToSpawn = 0; //Start the while loop
        while(symbolToSpawn < 3) //While there are fewer than three active symbols...
        {
            int spawn = Random.Range(0, spawnPositions.Length); //Generate a random number in the spawn position array
            if(occupied[spawn] == false) //If that spawn position is not occupied...
            {
                GameObject newObject = Instantiate(symbolPrefabs[chosenSymbols[symbolToSpawn]], spawnPositions[spawn].transform); //Create the symbol there
                occupied[spawn] = true; //Mark it as occupied
                activeSymbols[symbolToSpawn] = newObject; //Place it in the tracker array
                symbolToSpawn++; //Move on to the next symbol.
            }
        }
        timer = Random.Range(minSymbolMoveTime, maxSymbolMoveTime); //Initialize the timer.
    }

    public void SymbolMoving()
    {
        occupied = new bool[spawnPositions.Length];
        
        int symbolToMove = 0; //Start the while loop
        while(symbolToMove < 3) //While there are fewer than three active symbols...
        {
            int spawn = Random.Range(0, spawnPositions.Length); //Generate a random number in the spawn position array
            if(occupied[spawn] == false) //If that spawn position is not occupied...
            {
                activeSymbols[symbolToMove].transform.position = spawnPositions[spawn].transform.position; //Move to that position
                occupied[spawn] = true; //Mark it as occupied
                
                symbolToMove++; //Move on to the next symbol
            }
        }
        timer = Random.Range(minSymbolMoveTime, maxSymbolMoveTime); //Initialize the timer.
        //I hope to god this works
    }
    
    public void GameWin()
    {
        SceneManager.LoadScene(2);
    }
}
