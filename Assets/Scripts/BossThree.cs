using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThree : MonoBehaviour
{

    public GameObject miner;
    public Transform player;
    public GameObject growingBeam;
    public GameObject growingExplosion;
    public float hP = 3000;
    bool beamTimer = true;
    bool growthTimer = true;
    bool explosionTimer = true;
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 500;
    int radius = 100;
    float power = 100;
    public GameObject starChunk;
    public ParticleSystem surface;
    public ParticleSystem corona;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, gameObject.transform.position) <= MinDist && explosionTimer)
        {


            StartCoroutine("Explosion");
            explosionTimer = false;
        }

        //if (growthTimer)
        //StartCoroutine("Supernova");

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!GameObject.FindGameObjectWithTag("Miner"))
        {
            transform.LookAt(player.position);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        }

        if (Vector3.Distance(player.position, gameObject.transform.position) <= MinDist && beamTimer)
        {


            StartCoroutine("Beam");
            beamTimer = false;
        }
        if (hP <= 0)
        {
            Instantiate(starChunk, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }



    public IEnumerator Beam()
    {



        yield return new WaitForSecondsRealtime(1);
        gameObject.transform.LookAt(player.transform);
        Instantiate(growingBeam, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine("Timer");
    }
    public IEnumerator Explosion()
    {



        yield return new WaitForSecondsRealtime(5);
        Instantiate(growingExplosion, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine("Timer");
    }

    public IEnumerator ExplosionTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        explosionTimer = true;
    }
    public IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1);
        beamTimer = true;
    }

    public IEnumerator GrowthTimer()
    {
        yield return new WaitForSecondsRealtime(1);
        growthTimer = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Solar Flare")
        {
            
            hP -= 100;
        }
        else if(other.gameObject.tag == "Player Beam")
        {
            hP -= 300;
        }
    }

    public IEnumerator Supernova()
    {
        for (int i = 1; i < 100; i++)
        {
            yield return new WaitForSecondsRealtime(1f);
            gameObject.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            surface.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            corona.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            StartCoroutine("GrowthTimer");
        }
    }
}



