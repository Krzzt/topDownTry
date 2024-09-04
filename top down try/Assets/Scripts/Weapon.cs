using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public GameObject Player;
    public PlayerBehaviour PlayerBehaviourScript;
    public GameObject Bullet;
    public Transform firePoint;
    public Transform doublepos1;
    public Transform doublepos2;
    public float fireForce;
    public float bulletDamage;
    public float critchance = 10f;
    public float critmultiplier = 1.5f;
    public float critdamage;
    private float beforecrit;
    public float Damagemultiplier = 1;
    public int piercefortext = 1;

    public int grenadeAmount = 1;
    public int currGrenadeAmount;

    public Camera sceneCamera;

    public Vector3 Grenadepos;

    public GameObject grenade;
    public int GrenadeDamage;

    public TMP_Text dmg_text;
    public TMP_Text critchance_text;
    public TMP_Text critmult_text;
    public TMP_Text pierce_text;

    [SerializeField] public UnityEngine.UI.Image ImageCooldown;
    [SerializeField] public TMP_Text textCooldown;
    public TMP_Text amountGrenadeText;

    public float grenadetimer = 0;

    public float lifestealPercent;
    public int DmgToHeal;

    public float grenadeCooldown = 5;
    // Start is called before the first frame update

    private void Start()
    {
        textCooldown.gameObject.SetActive(false);
        ImageCooldown.fillAmount = 0;
      
    }
    private void Awake()
    {
        grenadeAmount = 1;
        currGrenadeAmount = grenadeAmount;
   
        Player = GameObject.FindWithTag("Player");
        PlayerBehaviourScript = Player.GetComponent<PlayerBehaviour>();
    }
    public void Fire(bool doubleshot)
    {
        if (doubleshot)
        {
            GameObject projectile1 = Instantiate(Bullet, doublepos1.position, firePoint.rotation);
            GameObject projectile2 = Instantiate(Bullet, doublepos2.position, firePoint.rotation);
            projectile1.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            projectile2.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
        else
        {
            GameObject projectile = Instantiate(Bullet, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }

        
    }

    
    public void IncreaseDamageMulti(float amount)
    {
        Damagemultiplier += amount;

        bulletDamage = (bulletDamage * Damagemultiplier);
        
        bulletDamage = bulletDamage+1;
    }

    public void IncreaseDamageAdd(int amount)
    {
        float newamount = (amount * Damagemultiplier);
        bulletDamage += newamount;
        
    }

    public void DecreaseDmg(float amount)
    {
        bulletDamage = (int)(bulletDamage* amount);
    }

    private void Update()
    {
     
        if (lifestealPercent > 0)
        {
            if (DmgToHeal >= (int)(100 / lifestealPercent))
            {
                
                PlayerBehaviourScript.PlayerHeal((int)(DmgToHeal * (lifestealPercent/100)));
                DmgToHeal = 0;
            }
        }
       
        beforecrit = bulletDamage * critmultiplier;
        critdamage = (int)beforecrit;
        dmg_text.SetText("Dmg: " + bulletDamage);
        critchance_text.SetText("Crit Chance: " + critchance);
        critmult_text.SetText("Crit Mult.: " + critmultiplier);
        pierce_text.SetText("Pierce: " + piercefortext);
        amountGrenadeText.SetText(currGrenadeAmount.ToString());
        if (currGrenadeAmount < grenadeAmount)
        {
            grenadetimer -= Time.deltaTime;
        }
      
        if (Input.GetKeyDown("space") && currGrenadeAmount > 0)
        {
            currGrenadeAmount--;
            FireGrenade();
           

        }
        if (grenadetimer > 0 && grenadeAmount != currGrenadeAmount)
        {
            textCooldown.gameObject.SetActive(true);
            ImageCooldown.fillAmount = grenadetimer / grenadeCooldown;
        }
        if (grenadetimer >= 0)
        {
            textCooldown.SetText(grenadetimer.ToString("F1"));
        }
       if (grenadetimer <= 0)
        {
            textCooldown.gameObject.SetActive(false);
            currGrenadeAmount++;
            grenadetimer = grenadeCooldown;

        }
       if (currGrenadeAmount >= grenadeAmount)
        {
            currGrenadeAmount = grenadeAmount;
        }


    }

    public void IncreaseCritChance(float amount)
    {
        critchance += amount;
    }

    public void IncreaseCritMultiplier(float amount)
    {
        critmultiplier += amount;
    }

    public void FireGrenade()
    {
        GameObject Grenade = Instantiate(grenade, firePoint.position, firePoint.rotation);
        
        Grenadepos = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void moreGrenades()
    {
        grenadeAmount++;
    }

    public void IncreaseLifeSteal()
    {
        lifestealPercent++;
    }

    public void ReduceCooldown(float amount)
    {
        grenadeCooldown -= amount;
    }

}
