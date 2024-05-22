using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable para la velocidad de movimiento
    public float playerSpeed = 5.5f;
    //Variable para la fuerza del salto
    public float jumpForce = 2.5f;

    //Variable para acceder al SpriteRenderer
    private SpriteRenderer spriteRenderer;
    //Variable para acceder al RigidBody2D
    private Rigidbody2D rBody;
    //Variable para acceder al GroundSensor
    private GroundSensor sensor;
    //Variable para acceder al Animator
    public Animator anim;

    //Variable para almacenar el input de movimiento
    float horizontal;


    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    private bool canShoot=true;
    public float timer;
    public float rateOfFire = 1;
    
    GameManager gameManager;

    void Awake()
    {
        //Asignamos la variables del SpriteRender con el componente que tiene este objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Asignamos la variable del Rigidbody2D con el componente que tiene este objeto
        rBody = GetComponent<Rigidbody2D>();
        //Buscamos un Objeto por su nombre, cojemos el Componente GroundSensor de este objeto y lo asignamos a la variable
        sensor = GameObject.Find("GroundSensor").GetComponent<GroundSensor>();
        //Buscamos el objeto del GameManager y SFXManager lo asignamos a las variables
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        anim=GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        if(gameManager.isGameOver == false)
        {
            horizontal = Input.GetAxis("Horizontal");

            if(horizontal < 0)
            {
                anim.SetBool("IsWalking",true);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if(horizontal > 0)
            {
                anim.SetBool("IsWalking",true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }else{
                anim.SetBool("IsWalking",false);
            }


            if(Input.GetButtonDown("Jump") && sensor.isGrounded)
            {
                anim.SetBool("IsWalking",false);
                anim.SetBool("IsJumping",true);
                rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }    
        
    }

    void FixedUpdate()
    {
        rBody.velocity = new Vector2(horizontal * playerSpeed, rBody.velocity.y);
    }

    void Shoot(){
        if(!canShoot){
            timer+=Time.deltaTime;
            if(timer>=rateOfFire){
                canShoot=true;
                timer=0;
            }
        }
        if(Input.GetKeyDown(KeyCode.F) && canShoot){
            Instantiate(bulletPrefab, bulletSpawn.position,bulletSpawn.rotation);
            canShoot = false;
        }
    }

    public void Die()
    {
        gameManager.GameOver();
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Coin")
        {
            gameManager.AddCoin();
            Destroy(collider.gameObject);
        }
    }
}
