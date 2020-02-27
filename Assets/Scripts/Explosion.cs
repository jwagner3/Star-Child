using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public GameObject player;
    public Vector3 scale;
    public ParticleSystem corona;
    public ParticleSystem surface;
    public ParticleSystem coronaR;
    public ParticleSystem surfaceR;
    

    void Awake()
    {
        StartCoroutine("Growth");
    }

    void Update()
    {
        //surface = GetComponent<ParticleSystem>();
        //corona = GetComponent<ParticleSystem>();
        //ParticleSystem.ShapeModule coronaR = corona.shape
        
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
            yield return new WaitForSecondsRealtime(.05f);
            gameObject.transform.localScale = new Vector3(i, i, i);
            corona.transform.localScale = new Vector3(i, i , i);
            surface.transform.localScale = new Vector3(i , i, i);
            
            
            if (i > 38)
            {
                             
                Destroy(gameObject);
            }
        }
       
    }
}