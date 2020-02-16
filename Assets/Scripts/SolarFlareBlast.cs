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

     BossOne bossOneScript;
    public GameObject bossOne;
    public float boss1HP = 1000;
    // Use this for initialization
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform.Rotate(0, 90, 0);
       
    }

    // Update is called once per frame
    void Update()
    {
        bossOneScript = bossOne.GetComponent<BossOne>();
        boss1HP = bossOne.GetComponent<BossOne>().hP;

        //It works!!! Projectiles move where you shoot them;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        
            if(collision.gameObject.tag == "Miner")
            {
                
                Destroy(collision.gameObject);
            }
        if(collision.gameObject.tag == "Boss")
        {
           
            if (boss1HP <= 0)
            {
                Debug.Log("hitBoss");
                Destroy(collision.gameObject);

            }

            boss1HP -= 100;
            Destroy(gameObject);
        }
    }
}