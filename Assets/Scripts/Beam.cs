using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour
{
    public GameObject player;
    public Vector3 scale;
    public GameObject bossTwo;
    public ParticleSystem corona;
    public ParticleSystem surface;
    Rigidbody rb;
    void Awake()
    {
        StartCoroutine("Growth");
        rb = gameObject.GetComponent<Rigidbody>();
        transform.Rotate(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

        
        //It works!!! Projectiles move where you shoot them;
        transform.Translate(Vector3.forward * Time.deltaTime * 50);


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
       
        for (int i = 1; i < 160; i++)
        {
            yield return new WaitForSecondsRealtime(.001f);
            gameObject.transform.localScale = new Vector3(10, 10, i);
            //corona.transform.localScale = new Vector3(i, i, i);
            surface.transform.localScale = new Vector3(3f, i, 3f);
            
            if (i > 150)
            {
                             
                Destroy(gameObject);
            }
        }
       
    }
}