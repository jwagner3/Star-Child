using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Public variable to store a reference to the player game object
    public float turnSpeed = 4.0f;
    public Transform player;

    public float height = 1f;
    public float distance = 2f;

    private Vector3 offsetX;
    private Vector3 offsetY;

    public void Start()
    {

        offsetX = new Vector3(0, height , distance);
        offsetY = new Vector3(0, 0, distance);

    }

   public void LateUpdate()
        {
            offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
            offsetY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
            transform.position = player.position + offsetX + offsetY;
            transform.LookAt(player.position);
        }
    
}
