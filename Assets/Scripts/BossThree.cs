using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThree : MonoBehaviour
{

    public GameObject miner;
    public Transform player;
    public GameObject growingBeam;
    public GameObject growingExplosion;
    public float maxHP = 3000;
    public float hP = 3000;
    bool beamTimer = true;
    bool growthTimer = true;
    bool explosionTimer = true;
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 300;
    int radius = 100;
    float power = 100;
    public GameObject starChunk;
    public ParticleSystem surface;
    public ParticleSystem corona;
    public GUIStyle bossBarStyle;
    public Texture2D bossBarTexture;
    private float bossBarLength;
    public GUIStyle bossNameStyle;
    public Font bossNameFont;
    public GUIStyle bossTimerStyle;
    public float superNovaHP = 3000;

    public void OnGUI()
    {
        bossBarStyle.onNormal.background = bossBarTexture;
        bossBarStyle.normal.textColor = Color.white;
        bossNameStyle.normal.textColor = Color.white;
        bossNameStyle.font = bossNameFont;
        GUI.Box(new Rect(260, 800, bossBarLength, 30), "Recitritus, Corpse of Worlds", bossNameStyle);
        GUI.Box(new Rect(260, 850, bossBarLength, 20), hP + "/" + maxHP, bossBarStyle);
        GUI.Box(new Rect(260, 900, Screen.width / 2, 20), "Time Until Supernova", bossTimerStyle);
    }
    void Start()
    {
        hP = maxHP;
        bossBarLength = Screen.width / 2;
        StartCoroutine("Supernova");
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (Vector3.Distance(player.position, gameObject.transform.position) <= MinDist && explosionTimer)
        {


            StartCoroutine("Explosion");
            explosionTimer = false;
        }

        
        

        
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
   
}



