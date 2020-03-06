using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMiner : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float minDistance = 200;
    public float moveSpeed = 30;

    private void Start()
    {
        
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 relativePos = (target.position + new Vector3(0, 1.5f, 0)) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion current = transform.localRotation;


        if (Vector3.Distance(transform.position, player.position) <= minDistance)
        {
            transform.LookAt(player);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {



            transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
            transform.Translate(0, 0, 8 * Time.deltaTime);
        }
        
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
    public IEnumerator Timer()
    {
        

        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }
}



