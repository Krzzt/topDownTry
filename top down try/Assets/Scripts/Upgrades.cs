using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Linq;

public class Upgrades : MonoBehaviour
{
    private GameObject Player;
    private GameObject Weapon;
    private GameObject ExpManager;
    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private Weapon weapon;
    [SerializeField] private movement movement;
    [SerializeField] private Bullet bullet;
    [SerializeField] private ExperienceManager experienceManager;
    [SerializeField] private EnemyBullet enemybullet;
    public GameObject Bullet;
    public GameObject enemiesBullet;
    public GameObject Boss;

    Upgrade Upgrade_1;
    Upgrade Upgrade_2;
    Upgrade Upgrade_3;
    Upgrade Upgrade_4;
    public Boolean Destroyenemybullets = false;

    public Sprite[] UpgradeSprites;

    public GameObject SkillGiverObject;
    public SkillGiver SkillGiverScript;

    public bool bossdead = false;
    // DEFINE LIST WITH UPGRADES
    Upgrade[] _CommonUpgrades = new Upgrade[]
    {

        //new Upgrade { Name = "Projectile size", Description = "Increases size of projectiles by a bit", Rarity = "Common", Increase = 30 },
        //new Upgrade { Name = "Crit multiplier", Description = "Crit does more damage by X%", Rarity = "Common", Increase = 25 },
        //new Upgrade { Name = "Health", Description = "Increases your hitpoints by X", Rarity = "Common", Increase = 25 },
        //new Upgrade { Name = "Heal", Description = "Heals you by X", Rarity = "Common", Increase = 500 },
        //new Upgrade { Name = "Increase Move Speed", Description = "Increase your Movement Speed by a bit", Rarity = "Common", Increase = 50 },
        //new Upgrade { Name = "Thorns", Description = "You do X more Damage to Enemies that hit you", Rarity = "Common", Increase = 15 },
        //new Upgrade { Name = "Increase Shot Speed", Description = "Your Shots are Faster", Rarity = "Common", Increase = 15 },
         new Upgrade { Name = "Pink Donut", Description = "+1 Damage, +3 Max HP", Rarity = "Common", Increase = 15, ID = 0 },
         new Upgrade { Name = "Healthy Sandwich", Description = "+5 MoveSpeed, +10 Max HP", Rarity = "Common", Increase = 15, ID = 1 },
         new Upgrade { Name = "Bigger Blobs", Description = "Bigger projectiles, + 15 Shot Speed", Rarity = "Common", Increase = 15, ID = 2 },

         new Upgrade { Name = "Health Potion", Description = "Heal for 200HP, +5 Max HP", Rarity = "Common", Increase = 15, ID = 4 },
         new Upgrade { Name = "Garlic", Description = "+10 Thorns DMG, +5 Max HP, Heal for 20HP", Rarity = "Common", Increase = 15, ID = 5 },
         new Upgrade { Name = "Sunflower Seeds", Description = "+1 Damage, +10 Shot Speed ", Rarity = "Common", Increase = 15, ID = 6 },
         new Upgrade { Name = "Milk", Description = "+10% Crit DMG, +5 Max HP ", Rarity = "Common", Increase = 15, ID = 7 },
         new Upgrade { Name = "A Cup of Tea", Description = "+2% Crit Chance, +4% Crit DMG", Rarity = "Common", Increase = 15, ID = 8 },
         new Upgrade { Name = "Cola Bottle", Description = "+0.1 Atk Speed, +5 moveSpeed", Rarity = "Common", Increase = 15, ID = 9 },
         new Upgrade { Name = "Carrots", Description = "+25 Max HP", Rarity = "Common", Increase = 15, ID = 10 },
         new Upgrade { Name = "Energy Drink", Description = "+10 moveSpeed", Rarity = "Common", Increase = 15, ID = 11 },


};

    List<Upgrade> CommonUpgrades = new List<Upgrade> 
    {
         new Upgrade { Name = "Pink Donut", Description = "+1 Damage, +3 Max HP", Rarity = "Common", Increase = 15, ID = 0 },
         new Upgrade { Name = "Healthy Sandwich", Description = "+5 MoveSpeed, +10 Max HP", Rarity = "Common", Increase = 15, ID = 1 },
         new Upgrade { Name = "Bigger Blobs", Description = "Bigger projectiles, + 15 Shot Speed", Rarity = "Common", Increase = 15, ID = 2 },

         new Upgrade { Name = "Health Potion", Description = "Heal for 200HP, +5 Max HP", Rarity = "Common", Increase = 15, ID = 4 },
         new Upgrade { Name = "Garlic", Description = "+10 Thorns DMG, +5 Max HP, Heal for 20HP", Rarity = "Common", Increase = 15, ID = 5 },
         new Upgrade { Name = "Sunflower Seeds", Description = "+1 Damage, +10 Shot Speed ", Rarity = "Common", Increase = 15, ID = 6 },
         new Upgrade { Name = "Milk", Description = "+10% Crit DMG, +5 Max HP ", Rarity = "Common", Increase = 15, ID = 7 },
         new Upgrade { Name = "A Cup of Tea", Description = "+2% Crit Chance, +4% Crit DMG", Rarity = "Common", Increase = 15, ID = 8 },
         new Upgrade { Name = "Cola Bottle", Description = "+0.1 Atk Speed, +5 moveSpeed", Rarity = "Common", Increase = 15, ID = 9 },
         new Upgrade { Name = "Carrots", Description = "+25 Max HP", Rarity = "Common", Increase = 15, ID = 10 },
         new Upgrade { Name = "Energy Drink", Description = "+10 moveSpeed", Rarity = "Common", Increase = 15, ID = 11 },



    };
    Upgrade[] _RareUpgrades = new Upgrade[]
  {
      new Upgrade { Name = "Attack speed (projectiles)", Description = "Increases Attack Speed by X Projectiles per Second", Rarity = "Rare", Increase = 0.2f },
       new Upgrade { Name = "Flat Projectile damage", Description = "Increases projectile damage by X", Rarity = "Rare", Increase = 3 },
         new Upgrade { Name = "Level up faster", Description = "Level up faster by X%", Rarity = "Rare", Increase = 10 },
           new Upgrade { Name = "Crit chance", Description = "Greater chance to do crit by X%", Rarity = "Rare", Increase = 7 },
            new Upgrade { Name = "Regeneration", Description = "Heals you by X HP every 5 Seconds", Rarity = "Rare", Increase = 1 },
              new Upgrade { Name = "Knockback", Description = "Enemies get knocked back on a hit (stackable)", Rarity = "Rare", Increase = 10 },
               new Upgrade { Name = "Increase Grenade Damage", Description = "Your Grenades do X more Damage", Rarity = "Rare", Increase = 4},
               new Upgrade{ Name = "Lower Exp Cost", Description = "Cuts the next Exp Requirement in half", Rarity = "Rare", Increase = 1 }
  };

    Upgrade[] _EpicUpgrades = new Upgrade[]
  {
        new Upgrade { Name = "Increase Pierce", Description = "Increase your Pierce by X", Rarity = "Epic", Increase = 1 },
        new Upgrade { Name = "Projectile damage (Multiplicative)", Description = "Increases projectile damage by X%", Rarity = "Epic", Increase = 15 },
         new Upgrade {Name = "Grenadier", Description = "Increases your Max Amount of Grenades by X", Rarity = "Epic", Increase = 1 },
        new Upgrade { Name = "Vampirism", Description = "Lets you Lifesteal for X% of your Bullet Damage done", Rarity = "Epic", Increase = 1 },
  };


    Upgrade[] _LegendaryUpgrades = new Upgrade[]
        {
            new Upgrade { Name = "Double Shot", Description = "X Shots!", Rarity = "Legendary", Increase = 2 },
            new Upgrade { Name = "Destroy Enemy Bullets", Description = "Destroys the Enemy Bullets if you Shoot at them", Rarity = "Legendary", Increase = 2 },
            new Upgrade { Name = "Sniper", Description = "+5 Pierce, -60% ATK Spd., +200% DMG, +100% Projectile Size", Rarity = "Legendary", Increase = 2 },
            new Upgrade { Name = "Minigun", Description = "+ 300% ATK Spd., -50% DMG", Rarity = "Legendary", Increase = 2}
        };




    [SerializeField] public Button Upgrade_button1;
    [SerializeField] public Button Upgrade_button2;
    [SerializeField] public Button Upgrade_button3;
    [SerializeField] public Button Upgrade_button4;

    [SerializeField] public Image Rarity_Panel1;
    [SerializeField] public Image Rarity_Panel2;
    [SerializeField] public Image Rarity_Panel3;
    [SerializeField] public Image Rarity_Panel4;

    [SerializeField] public TMP_Text Rarity_PanelText1;
    [SerializeField] public TMP_Text Rarity_PanelText2;
    [SerializeField] public TMP_Text Rarity_PanelText3;
    [SerializeField] public TMP_Text Rarity_PanelText4;



    [SerializeField] private TMP_Text Upgrade_DescriptionText1;
    [SerializeField] private TMP_Text Upgrade_DescriptionText2;
    [SerializeField] private TMP_Text Upgrade_DescriptionText3;
    [SerializeField] private TMP_Text Upgrade_DescriptionText4;

    [SerializeField] public Image ItemImage1;
    [SerializeField] public Image ItemImage2;
    [SerializeField] public Image ItemImage3;
    [SerializeField] public Image ItemImage4;

    private void Start()
    {
    
       
            //ButtonsSet();
        

       
    }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Weapon = GameObject.FindWithTag("Weapon");
        ExpManager = GameObject.FindWithTag("Exp");
        player = Player.GetComponent<PlayerBehaviour>();
        weapon = Weapon.GetComponent<Weapon>();
        movement = Player.GetComponent<movement>();
        bullet = Bullet.GetComponent<Bullet>();
        experienceManager = ExpManager.GetComponent<ExperienceManager>();
        enemybullet = enemiesBullet.GetComponent<EnemyBullet>();
        SkillGiverObject = GameObject.FindWithTag("Skillmanager");
        SkillGiverScript = SkillGiverObject.GetComponent<SkillGiver>();


       // if (SkillGiverScript.CandyBowl)
        //{
          //  CommonUpgrades.Add(new Upgrade { Name = "Candy Bowl", Description = "+0.1 Atk Speed, Higher Chance for Candy Spawn", Rarity = "Common", Increase = 15, ID = 3 });
      //  }
       
    }
    private void Update()
    {

    }
    private void LateUpdate()
    {
       
    }

    public void ButtonsSet(bool bossKill)
    {
        // CHOOSING UPGRADE FROM UPGRADE ARRAY
        List<int> availableUpgrades1 = new List<int>();
        List<int> availableUpgrades2 = new List<int>();
        List<int> availableUpgrades3 = new List<int>();
        List<int> availableUpgradesLegendary = new List<int>();
        for (int i = 0; i < CommonUpgrades.Count; i++)
        {
            availableUpgrades1.Add(i);
        }
        for (int i = 0; i < _RareUpgrades.Length; i++)
        {
            availableUpgrades2.Add(i);
        }
        for (int i = 0; i < _EpicUpgrades.Length; i++)
        {
            availableUpgrades3.Add(i);
        }
        for (int i = 0; i < _LegendaryUpgrades.Length; i++)
        {
            availableUpgradesLegendary.Add(i);
        }
        List<Upgrade> shuffledListCommon = CommonUpgrades.OrderBy(x => UnityEngine.Random.value).ToList();

        ShuffleList(availableUpgradesLegendary);

        ShuffleList(availableUpgrades1);
        ShuffleList(availableUpgrades2);
        ShuffleList(availableUpgrades3);
        int rand1 = UnityEngine.Random.Range(1, 101);
        int rand2 = UnityEngine.Random.Range(1, 101);
        int rand3 = UnityEngine.Random.Range(1, 101);
        int rand4 = UnityEngine.Random.Range(1, 101);


        if (!bossKill)
        {
            Debug.Log("JETZT SOLLTEN NORMALE UPGRADES KOMMEN");
            if (rand1 <= 75)
            {
                Upgrade_1 = shuffledListCommon[0];
            }
            else if (rand1 <= 95 && rand1 > 75)
            {
                Upgrade_1 = _RareUpgrades[availableUpgrades2[0]];
            }
            else if (rand1 > 95)
            {
                Upgrade_1 = _EpicUpgrades[availableUpgrades3[0]];
            }

            if (rand2 <= 75)
            {
                Upgrade_2 = shuffledListCommon[1];
            }
            else if (rand2 <= 95 && rand2 > 75)
            {
                Upgrade_2 = _RareUpgrades[availableUpgrades2[1]];
            }
            else if (rand2 > 95)
            {
                Upgrade_2 = _EpicUpgrades[availableUpgrades3[1]];
            }

            if (rand3 <= 75)
            {
                Upgrade_3 = shuffledListCommon[2];
            }
            else if (rand3 <= 95 && rand3 > 75)
            {
                Upgrade_3 = _RareUpgrades[availableUpgrades2[2]];
            }
            else if (rand3 > 95)
            {
                Upgrade_3 = _EpicUpgrades[availableUpgrades3[2]];
            }

            if (rand4 <= 75)
            {
                Upgrade_4 = shuffledListCommon[3];
            }
            else if (rand4 <= 95 && rand4 > 75)
            {
                Upgrade_4 = _RareUpgrades[availableUpgrades2[3]];
            }
            else if (rand4 > 95)
            {
                Upgrade_4 = _EpicUpgrades[availableUpgrades3[3]];
            }

        }
        else
        {
            Debug.Log("JETZT SOLLTEN LEGENDARY UPGRADES KOMMEN");
            Upgrade_1 = _LegendaryUpgrades[availableUpgradesLegendary[0]];
            Upgrade_2 = _LegendaryUpgrades[availableUpgradesLegendary[1]];
            Upgrade_3 = _LegendaryUpgrades[availableUpgradesLegendary[2]];
            Upgrade_4 = _LegendaryUpgrades[availableUpgradesLegendary[3]];
        }


        Debug.Log("Upgrade1: " + Upgrade_1.Name);
        Debug.Log("Upgrade2: " + Upgrade_2.Name);
        Debug.Log("Upgrade3: " + Upgrade_3.Name);
        Debug.Log("Upgrade4: " + Upgrade_4.Name);

        // Setting text
        Upgrade_button1.transform.GetChild(0).GetComponent<TMP_Text>().text = Upgrade_1.Name;
        Upgrade_button2.transform.GetChild(0).GetComponent<TMP_Text>().text = Upgrade_2.Name;
        Upgrade_button3.transform.GetChild(0).GetComponent<TMP_Text>().text = Upgrade_3.Name;
        Upgrade_button4.transform.GetChild(0).GetComponent<TMP_Text>().text = Upgrade_4.Name;

        // Replacing the X with increase value
        Upgrade_DescriptionText1.text = Upgrade_1.Description.Replace("X", Upgrade_1.Increase.ToString());
        Upgrade_DescriptionText2.text = Upgrade_2.Description.Replace("X", Upgrade_2.Increase.ToString());
        Upgrade_DescriptionText3.text = Upgrade_3.Description.Replace("X", Upgrade_3.Increase.ToString());
        Upgrade_DescriptionText4.text = Upgrade_4.Description.Replace("X", Upgrade_4.Increase.ToString());

        // Setting color of the buttons
        Dictionary<string, Color> rarityColors = new Dictionary<string, Color>();
        rarityColors.Add("Common", new Color(1, 1, 1, 1));
        rarityColors.Add("Rare", new Color(0.5f, 1f, 0.5f, 1));
        rarityColors.Add("Epic", new Color(0.75f, 0.25f, 0.75f, 1));
        rarityColors.Add("Legendary", new Color(1, 0.92f, 0.016f, 1));


        Rarity_Panel1.color = rarityColors[Upgrade_1.Rarity];
        Rarity_Panel2.color = rarityColors[Upgrade_2.Rarity];
        Rarity_Panel3.color = rarityColors[Upgrade_3.Rarity];
        Rarity_Panel4.color = rarityColors[Upgrade_4.Rarity];

        Rarity_PanelText1.text = Upgrade_1.Rarity.ToString();
        Rarity_PanelText2.text = Upgrade_2.Rarity.ToString();
        Rarity_PanelText3.text = Upgrade_3.Rarity.ToString();
        Rarity_PanelText4.text = Upgrade_4.Rarity.ToString();

        ItemImage1.sprite = UpgradeSprites[Upgrade_1.ID];
        ItemImage2.sprite = UpgradeSprites[Upgrade_2.ID];
        ItemImage3.sprite = UpgradeSprites[Upgrade_3.ID];
        ItemImage4.sprite = UpgradeSprites[Upgrade_4.ID];
    }

    // UPGRADES
    public void UpgradeChosen(string Upgrade_chosen)
    {
        if (Upgrade_chosen == "Attack speed (projectiles)")
        {
            movement.IncreaseAttackSpeed(0.2f);
            Debug.Log("Attack speed (projectiles)");
        }
        else if (Upgrade_chosen == "Projectile damage (Multiplicative)")
        {
            weapon.IncreaseDamageMulti(0.15f);
            Debug.Log("Projectile damage");
        }
        else if (Upgrade_chosen == "Flat Projectile damage")
        {
            weapon.IncreaseDamageAdd(3);
            Debug.Log("Projectile damage");
        }
        else if (Upgrade_chosen == "Pink Donut")
        {
            weapon.IncreaseDamageAdd(1);
            player._playerHealth.addmaxHealth(3);
        }
        else if (Upgrade_chosen == "Bigger Blobs")
        {
            bullet.transform.localScale += new Vector3(0.3f, 0.3f, 0);
            weapon.fireForce += 15;
        }
        else if (Upgrade_chosen == "Healthy Sandwich")
        {
            player._playerHealth.addmaxHealth(10);
            movement.IncreaseMoveSpeed(5);
        }
        else if (Upgrade_chosen == "Level up faster")
        {
            experienceManager.addMultiplier(0.1f);
            Debug.Log("Level up faster");
        }
        else if (Upgrade_chosen == "Crit chance")
        {
            weapon.IncreaseCritChance(7);
            Debug.Log(weapon.critchance);
        }
        else if (Upgrade_chosen == "Candy Bowl")
        {
            movement.IncreaseAttackSpeed(0.1f);
            //Higher Candy Chance
        }
        else if (Upgrade_chosen == "Increase Pierce")
        {

            bullet.IncreasePierce(1);
            weapon.piercefortext++;
        }
        else if (Upgrade_chosen == "Health Potion")
        {

            player.PlayerHeal(200);
            player._playerHealth.addmaxHealth(5);
        }
        else if (Upgrade_chosen == "Regeneration")
        {

            player.Regen(1);
        }
        else if (Upgrade_chosen == "Garlic")
        {

            player.IncreaseThornDamage(10);
            player._playerHealth.addmaxHealth(5);
            player.PlayerHeal(20);
        }
        else if (Upgrade_chosen == "Knockback")
        {

            player.knockback = true;
            player.knockbackStack++;
        }

        else if (Upgrade_chosen == "Double Shot")
        {

            movement.enableDoubleShot();
            movement.IncreaseAttackSpeed(-0.4f);
        }


        else if (Upgrade_chosen == "Destroy Enemy Bullets")
        {

            bullet.destruction = true;
            enemybullet.Destruction();
        }

        else if (Upgrade_chosen == "Sniper")
        {

            bullet.IncreasePierce(5);
            movement.DecreaseAttackSpeedMult(0.4f);
            weapon.IncreaseDamageMulti(2f);
            Bullet.transform.localScale = Bullet.transform.localScale * 2;

        }

        else if (Upgrade_chosen == "Minigun")
        {
            movement.DecreaseAttackSpeedMult(3f);
            weapon.DecreaseDmg(0.5f);


        }

        else if (Upgrade_chosen == "Sunflower Seeds")
        {
            weapon.IncreaseDamageAdd(1);
            weapon.fireForce += 10;

        }

        else if (Upgrade_chosen == "Increase Grenade Damage")
        {
            weapon.GrenadeDamage += 4;

        }

        else if (Upgrade_chosen == "Grenadier")
        {
            weapon.moreGrenades();

        }

        else if (Upgrade_chosen == "Vampirism")
        {
            weapon.IncreaseLifeSteal();

        }

        else if (Upgrade_chosen == "Lower Exp Cost")
        {

            player.maxExp = (player.maxExp / 1.25f) / 2;
            if (player.maxExp <= 1)
            {
                player.maxExp = 1.5f;
            }
        }

        else if (Upgrade_chosen == "Milk")
        {
            weapon.IncreaseCritMultiplier(0.1f);
            player._playerHealth.addmaxHealth(5);
        }

        else if (Upgrade_chosen == "A Cup of Tea")
        {
            weapon.IncreaseCritChance(2);
            weapon.IncreaseCritMultiplier(0.04f);
        }

        else if (Upgrade_chosen == "Cola Bottle")
        {
            movement.IncreaseAttackSpeed(0.1f);
            movement.IncreaseMoveSpeed(5);
        }

        else if (Upgrade_chosen == "Carrots")
        {
            player._playerHealth.addmaxHealth(25);
        }

        else if (Upgrade_chosen == "Energy Drink")
        {
            movement.IncreaseMoveSpeed(10);
        }
    }

    // SHUFFLE LIST
    public void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public class Upgrade
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public float Increase { get; set; }

        public int ID { get; set; }

       
    }
}

