using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFlareBlast : MonoBehaviour

{

    public Vector3 velocity;

    public float counters;

   

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += velocity * Time.deltaTime;
    }

   
    }