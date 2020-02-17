using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Public variable to store a reference to the player game object
    public float turnSpeed = 6.0f;
    public Transform player;

    public float height = 2f;
    public float distance = 4f;

    private Vector3 offsetX;
    private Vector3 offsetY;
    private Vector3 offsetZ;
   
    
    
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        offsetX = new Vector3(0, height , distance);
        offsetY = new Vector3(0, 0, distance);
        offsetZ = new Vector3(0, 0, distance);
        Cursor.lockState = CursorLockMode.Confined;
    }

   public void LateUpdate()
        {
        float distFromPlayer = Vector3.Distance(player.position, gameObject.transform.position);
        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
            offsetY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
        offsetZ = Quaternion.AngleAxis(Input.GetAxis("Zorical") * turnSpeed, Vector3.back) * offsetZ;
        transform.position = player.position + offsetX + offsetY;

        
            transform.LookAt(player.position);


        
        }
    
}
