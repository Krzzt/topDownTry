using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class EnemyNormal : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] Spawner Spawner;
    public GameObject player;
    public float speed;
    
    //public float detectradius = 5;
    private float distance;
    public float damage;
    public Weapon weaponscript;
    public GameObject waffe;
   [SerializeField] public  int healthtoStart;

    public float between = 1f;
    public UnitHealth EnemyNormalHealth = new UnitHealth(0, 0);
    private bool isKnockedBack;


    public bool Ranged;
    public float StopDistance = 3f;
    public float ShootDistance = 10f;
    public Transform firingPoint;
    public float fireRate;
    private float timeToFire;
    public GameObject Bullet;
    public float fireForce;

    private GameObject Gamemanager;
    private GameManager Gamemanagerscript;

    public int chanceforCandy = 0;


    float knocktimer = 0.2f;
    [SerializeField] private PlayerBehaviour Player;

    public ScriptableObject[] Candies = new ScriptableObject[5];


    public float expAmount;

    public float baseSpeed;
    void Awake()
    {
        
        player = GameObject.FindWithTag("Player");
        waffe = GameObject.FindWithTag("Weapon");
        weaponscript = waffe.GetComponent<Weapon>();
        Player = player.GetComponent<PlayerBehaviour>();
        spawner = GameObject.FindWithTag("WaveSpawner");
        Spawner = spawner.GetComponent<Spawner>();
        Gamemanager = GameObject.FindWithTag("GameManager");
        Gamemanagerscript = Gamemanager.GetComponent<GameManager>();
       
        EnemyNormalHealth.addmaxHealth(healthtoStart);
        between = (EnemyNormalHealth._currentMaxHealth * Spawner.EnemyHealthMultiplier);
        EnemyNormalHealth.addmaxHealth((int)between - EnemyNormalHealth._currentMaxHealth);
        damage *= Spawner.EnemyHealthMultiplier;
        baseSpeed = speed;


    }

    private void Start()
    {
        
    }
    void Update()
    {
      
       
        if (speed <= 0 && !isKnockedBack)
        {
            speed = baseSpeed;
        }
        if (isKnockedBack) 
        {
            knocktimer -= Time.deltaTime;
        }
        if (knocktimer <= 0f) 
        {
            isKnockedBack = false;
            knocktimer = 0.2f;
            
        }

        if (EnemyNormalHealth._currentHealth <= 0)
        {

            CandyDropChance();
                Die();
    
       
        }

        
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;



        // if (distance < detectradius) 
        //{
        if (!Ranged)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
       if (Ranged)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            if (Vector2.Distance(player.transform.position, transform.position) >= StopDistance)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                
            }
           
            if (Vector2.Distance(player.transform.position, transform.position) <= ShootDistance)
            {
                Shoot();
            }
        }
        //}
    }

    public void CandyDropChance()
    {
        if (Gamemanagerscript.CandyEnabled)
        {
            int rand = UnityEngine.Random.Range(chanceforCandy, 101);
            if (rand == 100)
            {
                int rand2 = UnityEngine.Random.Range(0, 5);
                switch (rand2)
                {
                    case 0:
                        Instantiate(Candies[0], gameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(Candies[1], gameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Candies[2], gameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Candies[3], gameObject.transform.position, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(Candies[4], gameObject.transform.position, Quaternion.identity);
                        break;
                }
            }
        }
    }

    public void Die()
    {
        ExperienceManager.Instance.AddExperience((float)expAmount);
        Gamemanagerscript.addscore((int)expAmount);
        Explode(gameObject.transform.position);
        Destroy(gameObject);
    
    }
    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            float rand = UnityEngine.Random.Range(10f, 25f);
            GameObject projectile = Instantiate(Bullet, firingPoint.position, firingPoint.rotation);
            Bullet.transform.localScale = new Vector3(rand, rand * 2, 1);
            fireForce = UnityEngine.Random.Range(200f, 400f);
            projectile.GetComponent<Rigidbody2D>().AddForce(firingPoint.up * fireForce, ForceMode2D.Impulse);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.gameObject.tag)
        {
            case "PlayerBullet":


                isKnockedBack = true;
                if (Player.knockback && isKnockedBack && speed >= 0)
                {

                    speed = -80 * Player.knockbackStack;
                }
                if (Player.chancetocrit <= weaponscript.critchance)
                {
                    EnemyNormalHealth.DamageUnit((int)weaponscript.critdamage);
                 
                   
                    
                }
                else
                {
                    EnemyNormalHealth.DamageUnit((int)weaponscript.bulletDamage);
                }


                break;




            case "Player":
                if (Player.thorns && Player.thornrefresh)
                {
                    EnemyNormalHealth.DamageUnit(Player.thornDamage);
                    DamagePopup.Create(Player.thornDamage, false, gameObject.transform.position);
                    Player.thorns = false;
                    Player.thornrefresh = false;
                }
                break;

        }

      
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time >= Player.lastDamageTick + 1 && collision.gameObject.tag == "Player")
        {
            Player.lastDamageTick = Time.time;
            Player.PlayerTakeDamage((int)damage);

        }
        if (Player.thornDamage > 0 && !Player.thorns)
        {
            Player.thorns = true;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Player.thorns && Player.thornrefresh)
        {
            EnemyNormalHealth.DamageUnit(Player.thornDamage);
            DamagePopup.Create(Player.thornDamage, false, gameObject.transform.position);
            Player.thorns = false;
            Player.thornrefresh = false;
        }
        if (Time.time >= Player.lastDamageTick + 1 && collision.gameObject.tag == "Player")
        {
            Player.lastDamageTick = Time.time;
            Player.PlayerTakeDamage((int)damage);

        }

    }



    private void EnemyHeal(int healing)
    {
        EnemyNormalHealth.HealUnit(healing);
    }


    public void Explode(Vector2 position)
    {
        Instantiate(DeathParticle, position, Quaternion.identity);

    }

    public void EnemyDamage(int amount)
    {
        EnemyNormalHealth.DamageUnit(amount);
    }
}
