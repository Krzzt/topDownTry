using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
public class LegendaryUpgrades : MonoBehaviour
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

    public bool bossdead = false;
    // DEFINE LIST WITH UPGRADES


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


    [SerializeField] private TMP_Text Upgrade_DescriptionText1;
    [SerializeField] private TMP_Text Upgrade_DescriptionText2;
    [SerializeField] private TMP_Text Upgrade_DescriptionText3;
    [SerializeField] private TMP_Text Upgrade_DescriptionText4;

    private void Start()
    {

       
            LegendaryButtons();
           
        
    

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

    }
    private void Update()
    {

    }


    public void LegendaryButtons()
    {
        bossdead = false;
        List<int> availableLegendaryUpgrades = new List<int>();
        for (int i = 0; i < _LegendaryUpgrades.Length; i++)
        {
            availableLegendaryUpgrades.Add(i);
        }

        ShuffleList(availableLegendaryUpgrades);
        Upgrade_1 = _LegendaryUpgrades[availableLegendaryUpgrades[0]];
        Upgrade_2 = _LegendaryUpgrades[availableLegendaryUpgrades[1]];
        Upgrade_3 = _LegendaryUpgrades[availableLegendaryUpgrades[2]];
        Upgrade_4 = _LegendaryUpgrades[availableLegendaryUpgrades[3]];

        Upgrade_button1.transform.GetChild(0).GetComponent<TMP_Text>().text = Upgrade_1.Name;
        Upgrade_button2.transform.GetChild(0).GetComponent<TMP_Text>().text = Upgrade_2.Name;
        Upgrade_button3.transform.GetChild(0).GetComponent<TMP_Text>().text = Upgrade_3.Name;
        Upgrade_button4.transform.GetChild(0).GetComponent<TMP_Text>().text = Upgrade_4.Name;

        Upgrade_DescriptionText1.text = Upgrade_1.Description.Replace("X", Upgrade_1.Increase.ToString());
        Upgrade_DescriptionText2.text = Upgrade_2.Description.Replace("X", Upgrade_2.Increase.ToString());
        Upgrade_DescriptionText3.text = Upgrade_3.Description.Replace("X", Upgrade_3.Increase.ToString());
        Upgrade_DescriptionText4.text = Upgrade_4.Description.Replace("X", Upgrade_4.Increase.ToString());

        Dictionary<string, Color> rarityColors = new Dictionary<string, Color>();
        rarityColors.Add("Common", new Color(1, 1, 1, 1));
        rarityColors.Add("Rare", new Color(0.5f, 1f, 0.5f, 1));
        rarityColors.Add("Epic", new Color(0.75f, 0.25f, 0.75f, 1));
        rarityColors.Add("Legendary", new Color(1, 0.92f, 0.016f, 1));

        Upgrade_button1.GetComponent<Image>().color = rarityColors[Upgrade_1.Rarity];
        Upgrade_button2.GetComponent<Image>().color = rarityColors[Upgrade_2.Rarity];
        Upgrade_button3.GetComponent<Image>().color = rarityColors[Upgrade_3.Rarity];
        Upgrade_button4.GetComponent<Image>().color = rarityColors[Upgrade_4.Rarity];
    }
   
    public void UpgradeChosen(string Upgrade_chosen)
    {
       

        if (Upgrade_chosen == "Double Shot")
        {

            movement.enableDoubleShot();
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

}

public class Upgrade
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Rarity { get; set; }
    public float Increase { get; set; }


}