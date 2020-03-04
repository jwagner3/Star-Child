using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float rotateSpeed = 1;
    public float maxHP = 1000;
    public float curHP;
    

    public GameObject growingExplosion;
    public GameObject beam;
    public bool explosionTimer = false;
    public bool beamTimer = false;

    public Scene Level2;

    public bool boostUsed = false;

    public Transform launcher;
    public GameObject projectile;
    public Rigidbody projectileR;
    public bool reloaded = true;
    public GameObject bossOne;

    private Vector3 moveDirection = Vector3.zero;
    
    private Quaternion moveRotation;
    public Texture2D healthBarTexture;
    public GUIStyle healthBarStyle;
    private float healthBarlength;
    public bool healthShow = true;

    Renderer darkMode;
    public Material bravoSix;
    public Material bravoSeven;

    public Camera mainCamera;

    public ParticleSystem corona;
    public ParticleSystem surface;

    public bool explosionNotifier = false;
    public GUIStyle notifierStyle;


    void OnGUI()
    {
        if (healthShow)
        {
            healthBarStyle.onNormal.background = healthBarTexture;
            GUI.Box(new Rect(260, 60, healthBarlength, 20), curHP + "/" + maxHP, healthBarStyle);
        }
        else
        {
            healthShow = true;
        }

        if (explosionNotifier)
        {
            notifierStyle.normal.textColor = Color.white;
            GUI.Box(new Rect(260, 300, 100, 20), "Press X to use your Cascade Abiity", notifierStyle);
            explosionNotifier = false;
        }
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
       darkMode = gameObject.GetComponent<Renderer>();
        curHP = maxHP;
        healthBarlength = Screen.width / 2;
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Load Scene 1" || SceneManager.GetActiveScene().name == "Load Scene 2" || SceneManager.GetActiveScene().name == "Load Scene 3")
        {
            Debug.Log("Scene");
            healthShow = false;
        }
        else
        {
            healthShow = true;
        }
        gameObject.transform.localScale = new Vector3(curHP / 500, curHP / 500, curHP / 500);
        corona.transform.localScale = new Vector3(curHP / 500, curHP / 500, curHP / 500);
        surface.transform.localScale = new Vector3(curHP / 500, curHP / 500, curHP / 500);
        if (curHP <= 0)
        {
            //SceneManager.LoadScene("Defeat");
        }
        CharacterController controller = GetComponent<CharacterController>();
        AdjustcurHealth(0);
        //Vector3 input = Input.mousePosition;
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(input.x, input.y, mainCamera.transform.position.y));
        //transform.LookAt(mousePosition + Vector3.up * transform.position.y);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {

            transform.LookAt(hit.point);
            
        }
        if (gameObject)
        Cursor.lockState = CursorLockMode.Confined;


        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Zorical"), Input.GetAxis("Vertical"));
            moveDirection = gameObject.transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
        
        
        controller.Move(moveDirection * Time.deltaTime);

        //speed boost
        if (Input.GetKeyDown(KeyCode.LeftShift) && !boostUsed)
        {
            speed = 10;
            boostUsed = true;
        }
        if (!boostUsed)
        {
            gameObject.GetComponent<Renderer>().material = bravoSeven;
            DynamicGI.UpdateEnvironment();
        }
        if (boostUsed)
        {
            gameObject.GetComponent<Renderer>().material = bravoSix;
            DynamicGI.UpdateEnvironment();
            StartCoroutine("BoostCooldown");
        }
        if (Input.GetMouseButtonDown(0) && reloaded)
        {
            Shoot(speed);
            curHP -= 20;
            reloaded = false;
            StartCoroutine("Reload");
            
        }
        if (!GameObject.FindGameObjectWithTag("Boss One"))
        {
            if (Input.GetKeyDown("c") && explosionTimer == false)
            {
                StartCoroutine("Explosion");

            }
            
            
            
        }

        if (!GameObject.FindGameObjectWithTag("Boss Two"))
        {
            if (!GameObject.FindGameObjectWithTag("Boss One"))
            {
               

                if (Input.GetKeyDown("z") && beamTimer == false)
                {
                    StartCoroutine("Beam");

                }
            }
        }

    }

    //miner deals damage on collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Miner" && !boostUsed)
        {
            curHP -= 150;
            Destroy(collision.gameObject);
            Debug.Log(curHP);
        }
    }


    //Health Bar controls
    public void AdjustcurHealth(float adj)
    {

        curHP += adj;
        if (curHP < 0)
            curHP = 0;
        if (curHP > maxHP)
            curHP = maxHP;
        if (maxHP < 1)
            maxHP = 1;
        healthBarlength = (Screen.width / 2) * (curHP / maxHP);
    }


    //Explosion deals damage on contact
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Explosion" && !boostUsed)
        {
            curHP -= 100;

        }
        if (other.gameObject.tag == "Bullet" && !boostUsed)
        {
            curHP -= 25;

        }
        if (other.gameObject.tag == "Beam" && !boostUsed)
        {
            curHP -= 200;

        }
        if (other.gameObject.tag == "Star Chunk 1")
        {
            SceneManager.LoadScene("Load Screen 2");
            //gameObject.transform.localScale = new Vector3(2, 2, 2);
            //surface.transform.localScale = new Vector3(2, 2, 2);
            //corona.transform.localScale = new Vector3(2, 2, 2);
            maxHP = 2000;
            curHP = maxHP;
            explosionNotifier = true;
            healthShow = false;
            
        }
        if (other.gameObject.tag == "Star Chunk 2")
        {
            SceneManager.LoadScene("Load Screen 3");
            //gameObject.transform.localScale = new Vector3(4, 4, 4);
            //surface.transform.localScale = new Vector3(4, 4, 4);
            //corona.transform.localScale = new Vector3(4, 4, 4);
            maxHP = 3000;
            curHP = maxHP;
            healthShow = false;

        }
        if (other.gameObject.tag == "Star Chunk 3")
        {
            SceneManager.LoadScene("Victory");
            //gameObject.transform.localScale = new Vector3(4, 4, 4);
            //surface.transform.localScale = new Vector3(4, 4, 4);
            //corona.transform.localScale = new Vector3(4, 4, 4);
            maxHP = 3000;
            curHP = maxHP;
            healthShow = false;
        }
    }

    public void Shoot(float speed)
    {
        //It works! It shoots the projectile right!
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
            bullet.transform.LookAt(hit.point);

           
            
        //    bullet.GetComponent<Rigidbody>().velocity = (_hit.point - transform.position).normalized * speed;
        }
    }

    //Boost ability Cooldown
    public IEnumerator BoostCooldown()
    {
        yield return new WaitForSecondsRealtime(5);
        boostUsed = false;
    }

    public IEnumerator Explosion()
    {
        yield return new WaitForSeconds(.1f);
        Instantiate(growingExplosion, gameObject.transform.position, gameObject.transform.rotation);
        curHP -= 100;
        StartCoroutine("Timer");
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(5);
        explosionTimer = false;
    }

    public IEnumerator Beam()
    {
        yield return new WaitForSeconds(.1f);
        Instantiate(beam, gameObject.transform.position, gameObject.transform.rotation);
        curHP -= 100;
        StartCoroutine("BeamTimer");
    }

    public IEnumerator BeamTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        beamTimer = false;
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(.5f);
        reloaded = true;
    }
}