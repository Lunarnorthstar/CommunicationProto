using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject theCamera;
    public GameObject[] allCameraPositions;
    public GameObject[] allEnemyPositions;
    public bool[] cameraActive;
    public bool[] cameraHaunted;

    public GameObject[] allEnemyTypes;

    public int currentCameraPos;
    public GameObject staticCameraOverlay;
    public GameObject globalButton;
    public GameObject allOtherButtons;

    public float minEnemyCooldown;
    public float maxEnemyCooldown;

    public float enemyTimer;


    // Start is called before the first frame update
    void Start()
    {
        cameraActive = new bool[allCameraPositions.Length];
        cameraHaunted = new bool[allCameraPositions.Length];
        cameraActive[0] = true;
        CameraChange(0);
        EnemySpawnReset();
    }

    // Update is called once per frame
    void Update()
    {
        CameraHandler();
        EnemyHandler();
    }

    void CameraHandler()
    {
        if (cameraActive[currentCameraPos] == false)
        {
            staticCameraOverlay.SetActive(true);
        }

        if (cameraActive[currentCameraPos] == true)
        {
            staticCameraOverlay.SetActive(false);
        }

        if (currentCameraPos == 0)
        {
            globalButton.SetActive(false);
            allOtherButtons.SetActive(true);
        }
        else
        {
            globalButton.SetActive(true);
            allOtherButtons.SetActive(false);
        }

        for (int i = 0; i < allCameraPositions.Length; i++)//For each camera...
        {
            if (cameraHaunted[i]) //If it's haunted...
            {
                cameraActive[i] = false; //deactivate it.
            }
        }
    }

    public void EnemyHandler()
    {
        enemyTimer -= Time.deltaTime;

        if (enemyTimer <= 0)
        {
            int spawn = Random.Range(1, allEnemyPositions.Length);
            
            if (!cameraHaunted[spawn])
            {
                GameObject newEnemy = Instantiate(allEnemyTypes[Random.Range(0, allEnemyTypes.Length)],
                    allEnemyPositions[spawn].transform);
                cameraHaunted[spawn] = true;
                newEnemy.GetComponent<EnemyBehavior>().room = spawn;
                EnemySpawnReset();
            }

        }
    }

    public void EnemySpawnReset()
    {
        enemyTimer = Random.Range(minEnemyCooldown, maxEnemyCooldown);
    }

    public void CameraChange(int cam)
    {
        if (cameraActive[cam])
        {
            theCamera.transform.position = allCameraPositions[cam].transform.position;
            currentCameraPos = cam;
        }
    }
}
