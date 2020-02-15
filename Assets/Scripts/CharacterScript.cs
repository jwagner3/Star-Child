using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float rotateSpeed = 1;
    public float maxHP = 1000;
    public float curHP;
    private float healthBarlenght;

    public bool boostUsed;

    public Transform launcher;
    public GameObject projectile;
    public Rigidbody projectileR;

    private Vector3 moveDirection = Vector3.zero;
    
    private Quaternion moveRotation;
    public Texture2D healthBarTexture;
    public GUIStyle healthBar;
     Renderer darkMode;
    public Color bravoSix;

    void OnGUI()
    {
        
        healthBar.onNormal.background = healthBarTexture;
        GUI.Box(new Rect(260, 60, healthBarlenght, 20), curHP + "/" + maxHP, healthBar);
       
    }

    public void Start()
    {
        bravoSix = Color.black;
        darkMode = gameObject.GetComponent<Renderer>();
        curHP = maxHP;
        healthBarlenght = Screen.width / 2;
    }
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        AdjustcurHealth(0);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            transform.LookAt(hit.point);

        }
        if(gameObject)
        Cursor.lockState = CursorLockMode.Confined;


        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Zorical"), Input.GetAxis("Vertical"));
            moveDirection = gameObject.transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
        
        
        controller.Move(moveDirection * Time.deltaTime);

        //speed boost
        if (Input.GetKeyDown(KeyCode.LeftShift) && !boostUsed)
        {
            speed *= 2;
            boostUsed = true;
        }
        if (boostUsed)
        {
            darkMode.material.SetColor("_EMISSION", Color.black);
            DynamicGI.UpdateEnvironment();
        }
        if (Input.GetKeyDown("r"))
        {
            Shoot(speed);
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
    }

    public void Shoot(float speed)
    {
        Vector3 screenMousePos = Input.mousePosition;

    // Turn that screen position into the actual position in the world.
    Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

    // Find out the direction between the player and the mouse pointer.
    Vector2 direction = mousePos - (Vector2)transform.position;
        // Normalize the direction and multiply by bullet speed.
        direction.Normalize();
        direction *= speed;

        // Spawn the bullet from the prefab.
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);

        // Find the BulletScript prefab on that spawned bullet, and set it's velocity component.
        bullet.GetComponent<SolarFlareBlast>().velocity = direction;
    }



}