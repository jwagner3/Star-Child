using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour
{

    public GameObject miner;
    public Transform player;
    public GameObject growingExplosion;
    public float hP = 1000;
    bool explosionTimer = true;
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 50;
    int radius = 100;
    float power = 100;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Miner"))
        {
            transform.LookAt(player.position);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            
        }

        if (Vector3.Distance(player.position, gameObject.transform.position) <= MinDist && explosionTimer)
        {
           

            StartCoroutine("Explosion");
            explosionTimer = false;
        }
    }



    public IEnumerator Explosion()
    {



        yield return new WaitForSecondsRealtime(5);
        Instantiate(growingExplosion, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine("Timer");
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(5);
        explosionTimer = true;  
    }
}
    

    
