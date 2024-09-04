using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class movement : MonoBehaviour
{
    public GameObject camera;
    
    public float moveSpeed;
    public Rigidbody2D rb;
    public float amplifier = 5f;
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    public Camera sceneCamera;
    public Weapon weapon;
    public float fireRate = 0.8f;
    private float lastShootTime = 0;
    public bool doubleshot = false;

    public float dashtimer = 0f;
    public float dashduration = 1f;
    private bool startdash = false;
    public TMP_Text firerate_text;
    // Update is called once per frame

    private void Awake()
    {
        camera = GameObject.FindWithTag("MainCamera");
        sceneCamera = camera.GetComponent<Camera>();
    }
    void Update()
    {

        ProcessInputs();
        move();
        firerate_text.SetText("Fire Rate: " + fireRate);
        if (dashtimer >= 0)
        {
            dashtimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashtimer <= 0)
        {
            moveSpeed = moveSpeed * amplifier;
            startdash = true;
           
        }

        if (startdash)
        {
            dashduration -= Time.deltaTime;

           
            if (dashduration <= 0)
            {
                moveSpeed = moveSpeed / amplifier;
                dashtimer = 0.5f;
                dashduration = 0.1f;
                startdash = false;
               
                
            }

        }
        if (Time.timeScale == 0)
        {
            if (startdash)
            {
                moveSpeed = moveSpeed / amplifier;
                dashtimer = 0.5f;
                dashduration = 0.1f;
                    startdash = false;
            }
        }
    }
    void FixedUpdate()
    {
       
    }
   
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
       
        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) && Time.time   >= lastShootTime + (1/fireRate))
        {
            lastShootTime = Time.time;
            weapon.Fire(doubleshot);
        }
    }
    void move()
    {
    
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;

    }

    public void IncreaseAttackSpeed(float amount)
    {
        fireRate += amount;
    }

    public void DecreaseAttackSpeedMult(float amount)
    {
        fireRate = fireRate * amount;
    }

    public void IncreaseMoveSpeed (float amount)
    {
        moveSpeed += amount;
    }

    public void enableDoubleShot()
    {
        doubleshot = true;
    }
}
