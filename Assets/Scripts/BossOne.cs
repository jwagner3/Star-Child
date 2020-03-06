using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour
{

    public GameObject miner;
    public Transform player;
    public GameObject growingExplosion;
    public float maxHP = 1500;
    public float hP = 1500;
    bool explosionTimer = true;
    int MoveSpeed = 8;
    int MaxDist = 10;
    int MinDist = 50;
    int radius = 100;
    float power = 100;
    public GameObject starChunk;
    public GUIStyle bossBarStyle;
    public Texture2D bossBarTexture;
    private float bossBarLength;
    public GUIStyle bossNameStyle;
    public Font bossNameFont;
    public ParticleSystem deathExplosion;

    public void OnGUI()
    {
        bossBarStyle.onNormal.background = bossBarTexture;
        bossBarStyle.normal.textColor = Color.white;
        bossNameStyle.normal.textColor = Color.white;
        bossNameStyle.font = bossNameFont;
        GUI.Box(new Rect(260, 950, bossBarLength, 30), "Lyrae, Tyrant of Uprising", bossNameStyle);
        GUI.Box(new Rect(260, 1000, bossBarLength, 20), hP + "/" + maxHP, bossBarStyle);
    }
    // Start is called before the first frame update
    void Start()
    {
        hP = maxHP;
        bossBarLength = Screen.width / 2;
    }
   
    // Update is called once per frame
    void Update()
    {

        AdjustcurHealth(0);
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        if(hP <= 0)
        {
           
            StartCoroutine("DeathTimer");
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
        yield return new WaitForSecondsRealtime(3);
        explosionTimer = true;  
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Solar Flare")
        {
            Debug.Log("hit");
            hP -= 100;
        }
    }
    public void AdjustcurHealth(float adj)
    {

        hP += adj;
        if (hP < 0)
            hP = 0;
        if (hP > maxHP)
            hP = maxHP;
        if (maxHP < 1)
            maxHP = 1;
        bossBarLength = (Screen.width / 2) * (hP / maxHP);
    }
    public IEnumerator DeathTimer()
    {
        deathExplosion.Play();

        yield return new WaitForSecondsRealtime(1);
        Instantiate(starChunk, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
    

    
