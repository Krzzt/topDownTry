using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpeed : MonoBehaviour
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

    private UnitHealth EnemyNormalHealth = new UnitHealth(15, 15);

    int expAmount = 2;

    public float between = 1f;


    [SerializeField] private PlayerBehaviour Player;
    
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        waffe = GameObject.FindWithTag("Weapon");
        weaponscript = waffe.GetComponent<Weapon>();
        Player = player.GetComponent<PlayerBehaviour>();
        spawner = GameObject.FindWithTag("WaveSpawner");
        Spawner = spawner.GetComponent<Spawner>();
        between = (EnemyNormalHealth._currentMaxHealth * Spawner.EnemyHealthMultiplier);
        EnemyNormalHealth.addmaxHealth((int)between - EnemyNormalHealth._currentMaxHealth);
        Debug.Log(EnemyNormalHealth._currentMaxHealth);

    }




    void Update()
    {

        if (EnemyNormalHealth._currentHealth <= 0)
        {
            Explode(gameObject.transform.position);
            Destroy(gameObject);
            ExperienceManager.Instance.AddExperience(expAmount);
        }



        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      
        

        // if (distance < detectradius) 
        //{
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.gameObject.tag)
        {
            case "PlayerBullet":

                if (Player.chancetocrit <= weaponscript.critchance)
                {
                    EnemyNormalHealth.DamageUnit((int)weaponscript.critdamage);
                }
                else
                {
                    EnemyNormalHealth.DamageUnit((int)weaponscript.bulletDamage);
                }
               

                break;






        }
        if (Player.thorns && Player.thornrefresh) 
        {
            EnemyNormalHealth.DamageUnit(Player.thornDamage);
            DamagePopup.Create(Player.thornDamage, false, gameObject.transform.position);
            Player.thorns = false;
            Player.thornrefresh = false;
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

    }



    private void EnemyHeal(int healing)
    {
        EnemyNormalHealth.HealUnit(healing);
    }

    public void Explode(Vector2 position)
    {
        Instantiate(DeathParticle, position, Quaternion.identity);

    }

}
