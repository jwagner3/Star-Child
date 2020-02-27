using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFlareBlast : MonoBehaviour

{
    public float speed = 1100;
    public Vector3 velocity;
    Rigidbody rb;
    public float counters;
    Vector3 setRotation;

    
    // Use this for initialization
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform.Rotate(0, 90, 0);
       
    }

    // Update is called once per frame
    void Update()
    {
        

        //It works!!! Projectiles move where you shoot them;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        
            if(collision.gameObject.tag == "Miner")
            {
                
               
            DestroyImmediate(gameObject);
        }
     
           if(collision.gameObject.tag == "Boss One")
            Destroy(gameObject);
        if (collision.gameObject.tag == "Boss Two")
            Destroy(gameObject);
        if (collision.gameObject.tag == "Boss Three")
            Destroy(gameObject);

    }
}