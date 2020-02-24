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
    private float healthBarlenght;

    public GameObject growingExplosion;
    public bool explosionTimer = false;

    public Scene Level2;

    public bool boostUsed = false;

    public Transform launcher;
    public GameObject projectile;
    public Rigidbody projectileR;
    public GameObject bossOne;

    private Vector3 moveDirection = Vector3.zero;
    
    private Quaternion moveRotation;
    public Texture2D healthBarTexture;
    public GUIStyle healthBar;
     Renderer darkMode;
    public Material bravoSix;
    public Material bravoSeven;

    public Camera mainCamera;

    void OnGUI()
    {
        
        healthBar.onNormal.background = healthBarTexture;
        GUI.Box(new Rect(260, 60, healthBarlenght, 20), curHP + "/" + maxHP, healthBar);
       
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
       darkMode = gameObject.GetComponent<Renderer>();
        curHP = maxHP;
        healthBarlenght = Screen.width / 2;
    }
    void Update()
    {
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
            speed = 5;
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
        if (Input.GetKeyDown("r"))
        {
            Shoot(speed);
            curHP -= 50;
            
        }
        if (!GameObject.FindGameObjectWithTag("Boss One"))
        {
            if (Input.GetKeyDown("c") && explosionTimer == false)
            {
                StartCoroutine("Explosion");
                
            }
        }

    }

    //miner deals damage on collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Miner")
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
        healthBarlenght = (Screen.width / 2) * (curHP / maxHP);
    }


    //Explosion deals damage on contact
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Explosion")
        {
            curHP -= 100;

        }
        if(other.gameObject.tag == "Star Chunk 1")
        {
            SceneManager.LoadScene("Level 2");
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
        yield return new WaitForSeconds(1);
        Instantiate(growingExplosion, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine("Timer");
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(5);
        explosionTimer = true;
    }
}