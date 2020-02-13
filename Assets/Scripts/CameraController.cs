using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 worldpos;
    private float mouseX;
    private float mouseY;
    private float cameraDif;

    public Camera mainCamera;

    public GameObject fpc;

    void Start()
    {
        cameraDif = mainCamera.transform.position.y - fpc.transform.position.y;
    }

    void LookAtMouse()
    {
        mouseX = Input.mousePosition.x;

        mouseY = Input.mousePosition.y;

        worldpos = mainCamera.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDif));

        Vector3 turretLookDirection = new Vector3(worldpos.x, fpc.transform.position.y, worldpos.z);

        fpc.transform.LookAt(turretLookDirection);
    }
}
