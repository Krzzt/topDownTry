using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBoss1 : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] Spawner Spawner;
    public PlayerBehaviour playerBehaviour;
    public GameObject player;
    public float speed;

    //public float detectradius = 5;
    private float distance;
    public int damage;
    public Weapon weaponscript;
    public GameObject waffe;


    public float between = 1f;
    public UnitHealth BossHealth = new UnitHealth(500, 500);



    public bool Ranged;
    public float StopDistance = 3f;
    public float ShootDistance = 40f;
    public Transform firingPoint;
    public Transform firingPoint2;
    public Transform firingPoint3;
    public float fireRate;
    private float timeToFire;
    public GameObject Bullet;
    public float fireForce;
    public float timerbetweenattacks = 1.5f;
    private bool attack = false;
    private bool dash = false;

    private float dashtimer;
    private float attack2timer;
 
    Vector3 playerpos;
    [SerializeField] private PlayerBehaviour Player;


    private Vector3 oldplayerpos;
    private bool once = false;


    public int expAmount;

    public int IncreaseStartHealth;
    void Awake()
    {

        player = GameObject.FindWithTag("Player");
        waffe = GameObject.FindWithTag("Weapon");
        weaponscript = waffe.GetComponent<Weapon>();
        Player = player.GetComponent<PlayerBehaviour>();
        spawner = GameObject.FindWithTag("WaveSpawner");
        Spawner = spawner.GetComponent<Spawner>();
        expAmount = Spawner.getWaveValue();
        

        IncreaseStartHealth = (int)(Spawner.currWave * Spawner.currWave);
        BossHealth.addmaxHealth(IncreaseStartHealth);

    }

    private void Start()
    {

    }
    void FixedUpdate()
    {
            if (timerbetweenattacks > 0)
        {
            timerbetweenattacks -= Time.deltaTime;

        }

        if (dash)
        {
            dashtimer -= Time.deltaTime;
            once = true;
            if (oldplayerpos == this.transform.position || once)
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (speed * 4) * Time.deltaTime);
        }

        if (timerbetweenattacks <= 0)
        {
            int randomtemp = 0;
            if (!attack)
            {
                
                dashtimer = 3f;
                attack2timer = 3f;
               
                playerpos = player.transform.position;
               

            }
            attack = true;
            randomtemp = UnityEngine.Random.Range(1, 3);


            switch (randomtemp)
            {
                case 1:
                    oldplayerpos = playerpos;
                    dash = true;
                    

                        transform.position = Vector2.MoveTowards(this.transform.position, playerpos, (speed * 4) * Time.deltaTime);
                        
                        if (dashtimer <= 0)
                        {
                            dash = false;
                            Debug.Log("COCK");
                            timerbetweenattacks = 3;
                            attack = false;
                        once = false;
                            break;
                        }
                    
                  
                
                    
                

                    break;

                case 2:

                   
                        attack2timer -= Time.deltaTime;
                        Shoot();
                        if (attack2timer <= 0)
                        {
                            Debug.Log("SCHWOANZ");
                            timerbetweenattacks = 3;
                            attack = false;
                            
                            break;
                        }
                    

                    break;

                
            }
        }
        
        

        if (BossHealth._currentHealth <= 0)
        {
            
            LegendaryUpgrade();
            Explode(gameObject.transform.position);
            ExperienceManager.Instance.AddExperience(expAmount);
            Destroy(gameObject);
            
        
        }


        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;



        // if (distance < detectradius) 
        //{

       
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            if (!dash)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
             

            

            //if (Vector2.Distance(player.transform.position, transform.position) <= ShootDistance)
            //{
              //  Shoot();
           // }



      
        //}
    }


    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            float rand1 = UnityEngine.Random.Range(-15, 15);
            float rand2 = UnityEngine.Random.Range(-15, 15);
            firingPoint.transform.Rotate(rand1, rand2, 0);
            Bullet.transform.localScale = new Vector3(15f, 30f, 1);
            GameObject projectile = Instantiate(Bullet, firingPoint.position, firingPoint.rotation);
            GameObject projectile2 = Instantiate(Bullet, firingPoint2.position, firingPoint2.rotation);
            GameObject projectile3 = Instantiate(Bullet, firingPoint3.position, firingPoint3.rotation);

            fireForce = 350f;
            projectile.GetComponent<Rigidbody2D>().AddForce(firingPoint.up * fireForce, ForceMode2D.Impulse);
            projectile2.GetComponent<Rigidbody2D>().AddForce(firingPoint2.up * fireForce, ForceMode2D.Impulse);
            projectile3.GetComponent<Rigidbody2D>().AddForce(firingPoint3.up * fireForce, ForceMode2D.Impulse);
            timeToFire = fireRate;
            firingPoint.transform.Rotate(rand1*(-1), rand2*(-1), 0);
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



                if (Player.chancetocrit <= weaponscript.critchance)
                {
                    BossHealth.DamageUnit((int)weaponscript.critdamage);



                }
                else
                {
                    BossHealth.DamageUnit((int)weaponscript.bulletDamage);
                }


                break;






        }


        if (Player.thorns && Player.thornrefresh)
        {
            BossHealth.DamageUnit(Player.thornDamage);
            DamagePopup.Create(Player.thornDamage, false, gameObject.transform.position);
            Player.thorns = false;
            Player.thornrefresh = false;
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
            BossHealth.DamageUnit(Player.thornDamage);
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


    public void LegendaryUpgrade()
    {
  
        playerBehaviour.UpgradeScreenBossKill();

        
    }


    private void EnemyHeal(int healing)
    {
        BossHealth.HealUnit(healing);
    }


    public void Explode(Vector2 position)
    {
        Instantiate(DeathParticle, position, Quaternion.identity);

    }
}
