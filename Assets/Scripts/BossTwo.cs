using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwo : MonoBehaviour
{

    public GameObject miner;
    public Transform player;
    public GameObject growingBeam;
    public float maxHP = 2000;
    public float hP = 2000;
    bool beamTimer = true;
    int MoveSpeed = 4;
    int MaxDist = 10;
    int MinDist = 100;
    int radius = 100;
    float power = 100;
    public GameObject starChunk;

    public GUIStyle bossBarStyle;
    public Texture2D bossBarTexture;
    private float bossBarLength;
    public GUIStyle bossNameStyle;
    public Font bossNameFont;

    public void OnGUI()
    {
        bossBarStyle.onNormal.background = bossBarTexture;
        bossBarStyle.normal.textColor = Color.white;
        bossNameStyle.normal.textColor = Color.white;
        bossNameStyle.font = bossNameFont;
        GUI.Box(new Rect(260, 800, bossBarLength, 30), "Solaire, Celestial of War", bossNameStyle);
        GUI.Box(new Rect(260, 850, bossBarLength, 20), hP + "/" + maxHP, bossBarStyle);
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

    public IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1);
        beamTimer = true;
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

}



