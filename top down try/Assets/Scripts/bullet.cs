using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] UnitHealth health;
    [SerializeField] ParticleSystem onHitParticle;
    [SerializeField] private Transform pfDamagePopup;
    [SerializeField] PlayerBehaviour Player;
    [SerializeField] Weapon Weapon;

    GameObject player;
    GameObject weapon;
    public Rigidbody2D rb;
    public int pierce = 1;
    public int pierceremaining = 1;
    public bool crit;
    public Boolean destruction = false;

   

    DamagePopup damagePopup;
    Transform damagePopupTransform;
    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Player = player.GetComponent<PlayerBehaviour>();
        weapon = GameObject.FindWithTag("Weapon");
        Weapon = weapon.GetComponent<Weapon>();
      
        if (Player.chancetocrit <= Weapon.critchance)
        {
            crit = true;

        }
        
        pierceremaining = pierce;
        
    }

    private void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
      switch(other.gameObject.tag)
        {
            case "Wall":
               
                    Destroy(gameObject);
                    
               
                break;
            case "Enemy":
               
                
                if (crit)
                {
                    DamagePopup.Create( (int)Weapon.critdamage, crit, gameObject.transform.position);
                    Weapon.DmgToHeal += (int)Weapon.critdamage;

                }
                else
                {
                    DamagePopup.Create( (int)Weapon.bulletDamage, crit , gameObject.transform.position);
                    Weapon.DmgToHeal += (int)Weapon.bulletDamage;
                }
                
                Explode(gameObject.transform.position);
                pierceremaining--;
                if (pierceremaining == 0)
                {
                   
                    Destroy(gameObject);
                }
                
                break;
            case "EnemyBullet":
                if (destruction)
                {
                    pierceremaining--;
                }
                if (pierceremaining == 0)
                {
                    Destroy(gameObject);
                }
                
                break;

               
        }

    }

    public void IncreasePierce(int amount)
    {
        pierce += amount;
        pierceremaining = pierce;
    }



    public void Explode(Vector2 position)
    {
        Instantiate(onHitParticle, position, Quaternion.identity);
       
    }

 

}
