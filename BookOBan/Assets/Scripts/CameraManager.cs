using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera myCamera;
    public Transform[] allCameraPositions;
    public bool[] cameraActive;
    public int currentCameraPos = 0;

    public GameObject staticCameraOverlay;
    public GameObject globalButton;
    public GameObject allOtherButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraActive = new bool[allCameraPositions.Length];
        cameraActive[0] = true;
        ButtonInput(0);
    }

    // Update is called once per frame
    void Update()
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
    }

    public void ButtonInput(int cam)
    {
        if (cameraActive[cam])
        {
            myCamera.transform.position = allCameraPositions[cam].position;
            currentCameraPos = cam;
        }
    }
}
