using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{

    private Camera MainCam;
    private Vector2 touchStart;
    public float speed;
    public Vector3 minValues,maxValues;

    void Start()
    {
        // Assign main camera
        MainCam = Camera.main;
        MainCam.enabled = true;
    }
    

    void Update()
    {
        // If finger is on screen and has moved from start position
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Move camera along x-axis only
            MainCam.transform.Translate(-touchStart.x * speed, 0, 0);

            // Camera Boundaries
            MainCam.transform.position = new Vector3(
                Mathf.Clamp(MainCam.transform.position.x,minValues.x,maxValues.x),
                Mathf.Clamp(MainCam.transform.position.y,minValues.y,maxValues.y),
                Mathf.Clamp(MainCam.transform.position.z,minValues.z,maxValues.z)
            );

        }

    }
}
