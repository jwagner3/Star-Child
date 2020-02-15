using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public GameObject player;
    public Vector3 scale;


    void Awake()
    {
        StartCoroutine("Growth");
    }

    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator Growth()
    {
       
        for (int i = 1; i < 40; i++)
        {
            yield return new WaitForSecondsRealtime(.1f);
            gameObject.transform.localScale = new Vector3(i, i, i);
            if (i > 38)
            {
                             
                Destroy(gameObject);
            }
        }
       
    }
}