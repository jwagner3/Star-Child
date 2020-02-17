using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class Miner : MonoBehaviour
{


    public AudioSource minerNoise;
    public Transform Player;
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 5;

    public ParticleSystem deathExplosion;

    public CharacterScript playerScript;
    public float playerHP;


    void Start()
    {
        minerNoise.Play();
        deathExplosion.Stop();   
    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Solar Flare")
        {

            StartCoroutine("Timer");

        }
    }


    public IEnumerator Timer()
    {
        deathExplosion.Play();
        if (deathExplosion.isPlaying == true)
            Debug.Log("spicy");
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }

}

