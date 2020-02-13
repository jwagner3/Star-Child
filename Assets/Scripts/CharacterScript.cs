using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float rotateSpeed = 1;


 
    private Vector3 moveDirection = Vector3.zero;
    private Quaternion moveRotation;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            transform.LookAt(hit.point);

        }




        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Zorical"), Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
        
        //Jumping
        controller.Move(moveDirection * Time.deltaTime);

    }



    
       
    }