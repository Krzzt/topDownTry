using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] UnitHealth health;
    [SerializeField] ParticleSystem onHitParticle;
    [SerializeField] private Transform pfDamagePopup;
    [SerializeField] PlayerBehaviour Player;
    [SerializeField] Weapon Weapon;

    public CircleCollider2D collision;
    EnemyNormal[] enemyNormals = new EnemyNormal[1000];
    List<GameObject> currentCollisions = new List<GameObject>();
   
    int x = 0;
    

    GameObject player;
    GameObject weapon;
    public Rigidbody2D rb;

    
    private bool second = true;

    public bool bossHit;
    public MinigunBoss1 minigunBossScript;

    bool locationreached = false;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Player = player.GetComponent<PlayerBehaviour>();
        weapon = GameObject.FindWithTag("Weapon");
        Weapon = weapon.GetComponent<Weapon>();
        transform.position = Vector2.MoveTowards(this.transform.position, Weapon.Grenadepos, Weapon.fireForce*2 * Time.deltaTime);
        collision = gameObject.GetComponent<CircleCollider2D>();
        minigunBossScript = GameObject.FindFirstObjectByType<MinigunBoss1>();
       // collision.radius = 0;
      
        second = true;
        x = 0;


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && !currentCollisions.Contains(other.gameObject))
        {
            
            currentCollisions.Add(other.gameObject);
            if (other.gameObject.TryGetComponent(out EnemyNormal en))
            {
                enemyNormals[x] = other.gameObject.GetComponent<EnemyNormal>();
            }
            else if(other.gameObject.TryGetComponent(out MinigunBoss1 Mn))
            {
                bossHit = true;
            }
            
          
            
            x++;
          
          
        }
      
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentCollisions.Remove(other.gameObject);
        for(int i = 0; i < enemyNormals.Length; i++)
        {
            if (enemyNormals[i] == other.gameObject.GetComponent<EnemyNormal>()) 
            {
                enemyNormals[i] = null;
               
            }
        }
      
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && !currentCollisions.Contains(other.gameObject))
        {
            currentCollisions.Add(other.gameObject);
            enemyNormals[x] = other.gameObject.GetComponent<EnemyNormal>();
            
            x++;
            

        }
     


    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, Weapon.Grenadepos, Weapon.fireForce * 2 * Time.deltaTime);
        if (Vector2.Distance(this.transform.position, Weapon.Grenadepos) <= 2f)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Weapon.Grenadepos, Weapon.fireForce * 2 * Time.deltaTime);

            //if (Physics2D.OverlapCircle(this.transform.position, 5f, 0, -Mathf.Infinity, Mathf.Infinity))
            //{

            //}

         
           
            

           if (second && Vector2.Distance(this.transform.position, Weapon.Grenadepos) <= 0 && currentCollisions.Count != 0)
            {
                if (bossHit)
                {
                    minigunBossScript.BossHealth.DamageUnit(Weapon.GrenadeDamage);
                    DamagePopup.Create(Weapon.GrenadeDamage, false, minigunBossScript.gameObject.transform.position);

                }
                second = false;
                for (int i = 0; i < enemyNormals.Length; i++)
                {
                    if (enemyNormals[i] != null)
                    {
                        enemyNormals[i].EnemyDamage(Weapon.GrenadeDamage);
                        DamagePopup.Create(Weapon.GrenadeDamage, false, enemyNormals[i].gameObject.transform.position);
                    }
         
                }
             
          
            } 

            Explode(gameObject.transform.position);
            Destroy(gameObject);






        }


      

    }
    public void Explode(Vector2 position)
    {
        Instantiate(onHitParticle, position, Quaternion.identity);

    }

  
 
}
