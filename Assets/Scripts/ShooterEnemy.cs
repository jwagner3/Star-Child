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
    public int enemySpeed = 8;
    int maxDist = 45;
    int minDist = 30;
    float enemyDist;
    GameObject enemy;
    float timer;
    bool frozen = false;
    public ParticleSystem deathExplosion;

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
        checkDistance();
        transform.LookAt(player.transform);
        transform.position += transform.forward * enemySpeed * Time.deltaTime;
        print(enemyDist);



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
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Solar Flare")
        {

            StartCoroutine("Timer");

        }
        if (collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "Player Explosion")
        {
            StartCoroutine("Timer");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            DestroyImmediate(gameObject);
        }
    }

    public IEnumerator Timer()
    {
        deathExplosion.Play();

        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }



}

