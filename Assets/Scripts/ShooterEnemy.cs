using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{

    // Variable Declaration
    public GameObject eProjectile;
    Transform eLauncher;
    public double eReloadTime = 100.0;
    bool eReady = true;
    double eReloadTimed;
    float erandomNum;
    public Transform player;
    public int enemySpeed = 3;
    int maxDist = 15;
    int minDist = 10;
    float enemyDist;
    GameObject enemy;
    float timer;
    bool frozen = false;


    public float ecounter;

    public float eRandomNum
    {
        get
        {
            return erandomNum;
        }

        set
        {
            erandomNum = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        eReloadTimed = 2;
        eLauncher = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyDist = Vector3.Distance(transform.position, player.transform.position);
        if (frozen != true)
        {
            checkDistance();
            if (enemyDist <= minDist)
            {
                transform.LookAt(player.transform);
                transform.position += transform.forward * enemySpeed * Time.deltaTime;
                print(enemyDist);



            }
            else
            {

            }


            eCheckReload();
            if (eReady == true && enemyDist <= minDist)
            {
                eReady = false;
                eFireProjectile();
                ecounter += 1;
            }
            else
            {
                eReloadTimed += (.8 + eRandomNum);

            }
        }
        else
        {
            if (Time.time > timer + 5)
            {
                frozen = false;
            }
        }
        Destroy(eProjectile, 1);
    }

    void eFireProjectile()
    {


        Instantiate(eProjectile, eLauncher.position, eLauncher.rotation);



    }


    void eCheckReload()
    {
        if (eReloadTimed > eReloadTime)
        {
            eReloadTimed = 0;
            eReady = true;
        }
    }
    void checkDistance()
    {
        float enemyDist = Vector3.Distance(transform.position, player.transform.position);
    }
    public void Freeze()
    {
        frozen = true;
        timer = Time.time;
    }


   
}

