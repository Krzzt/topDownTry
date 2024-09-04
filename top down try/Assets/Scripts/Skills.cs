using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Skills : MonoBehaviour
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
    public GameObject grenade;
    [SerializeField] private  Grenade GrenadeScript;

    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text price;
    public TMP_Text BuyText;

    public string Skillchosen;
    public string Skilldescription;
    public int Price;
    public string buy;



    public TMP_Text currentScore;


    public TMP_Text Test;
    public TMP_Text Test2;

    private SaveScore currScore;
    private SkillToSave saveSkillUnlocks;
    private void Awake()
    {
        currScore = new SaveScore { };
        saveSkillUnlocks = new SkillToSave {  };
        SaveSystem.checkIfExists("/saveSkills.txt");
        SaveSystem.checkIfExists("/Score.txt");
        SaveSystem.LoadSkills(saveSkillUnlocks);
        SaveSystem.LoadScore(currScore);
        if (saveSkillUnlocks.isUnlocked == null)
        {
            saveSkillUnlocks.isUnlocked = new bool[200];
        }
        else
        {
            for (int i = 0; i < normal_Skills.Length; i++)
            {
                normal_Skills[i].Unlocked = saveSkillUnlocks.isUnlocked[i];

            }
        }
     





        if (normal_Skills[0].Unlocked)
        {
            normal_Skills[1].Locked = false;
            normal_Skills[2].Locked = false;
            if (normal_Skills[1].Unlocked)
            {
                normal_Skills[3].Locked = false;
                if (normal_Skills[3].Unlocked)
                {
                    normal_Skills[5].Locked = false;
                    if (normal_Skills[5].Unlocked)
                    {
                        
                    }
                }
            }
            if (normal_Skills[2].Unlocked)
            {
                normal_Skills[4].Locked = false;
                if (normal_Skills[4].Unlocked)
                {
                    normal_Skills[6].Locked = false;
                    if (normal_Skills[6].Unlocked)
                    {
                       
                    }
                }
            }
        }


    }
   public  Skill[] normal_Skills = new Skill[]
    {
        new Skill{Name = "Grenadier", Description = "You Start with 1 extra Grenade", Locked = false, Price = 2000, Unlocked = false},
        new Skill{Name = "Healthy Diet", Description = "You Start with 15 Extra Health", Locked = true, Price = 3000, Unlocked = false},
        new Skill{Name = "Favorite Candy", Description = "You Start with +1 Health Regen", Locked = true, Price = 5000, Unlocked = false},
        new Skill{Name = "", Description = "", Locked = true, Price = 100, Unlocked = false},
        new Skill{Name = "", Description = "", Locked = true, Price = 100, Unlocked = false},
        new Skill{Name = "", Description = "", Locked = true, Price = 100, Unlocked = false},
        new Skill{Name = "", Description = "", Locked = true, Price = 100, Unlocked = false},








    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DontLock();

        currentScore.SetText("Your Score: " + currScore.ScoreAmount.ToString());
       // SaveSkills();
      

   
    }

    public void Skillchoose(string Skill)
    {
        Skill = Skillchosen;
        Debug.Log(Skill);
        if (Skill != null)
        {
            SkillBuy(Skill);
        }
       
    }


    
    public void SkillBuy(string SkillChosen)
    {
        if (SkillChosen == "Grenadier" && !normal_Skills[0].Unlocked && currScore.ScoreAmount >= Price && !normal_Skills[0].Locked)
        {
            //weapon.ReduceCooldown(1f);
            normal_Skills[0].Unlocked = true;
            if (normal_Skills[0].Unlocked)
            {
                for (int i = 0; i < normal_Skills.Length; i++)
                {
                    saveSkillUnlocks.isUnlocked[i] = normal_Skills[i].Unlocked;
                }
                SaveSystem.SaveSkills(saveSkillUnlocks);
            }

            currScore.ScoreAmount -= Price;
            SaveSystem.SaveScore(currScore);
            BuyText.SetText("Bought!");
            normal_Skills[1].Locked = false;
            normal_Skills[2].Locked = false;
        }
        if (SkillChosen == "Healthy Diet" && !normal_Skills[1].Unlocked && currScore.ScoreAmount >= Price && !normal_Skills[1].Locked)
        {
            //weapon.ReduceCooldown(1f);
            normal_Skills[1].Unlocked = true;
            if (normal_Skills[1].Unlocked)
            {
                for (int i = 0; i < normal_Skills.Length; i++)
                {
                    saveSkillUnlocks.isUnlocked[i] = normal_Skills[i].Unlocked;
                }
                SaveSystem.SaveSkills(saveSkillUnlocks);
            }
            currScore.ScoreAmount -= Price;
            SaveSystem.SaveScore(currScore);

            BuyText.SetText("Bought!");
            normal_Skills[3].Locked = false;
        }
        if (SkillChosen == "Favorite Candy" && !normal_Skills[2].Unlocked && currScore.ScoreAmount >= Price && !normal_Skills[2].Locked)
        {
            //weapon.ReduceCooldown(1f);
            normal_Skills[2].Unlocked = true;
            if (normal_Skills[2].Unlocked)
            {
                for (int i = 0; i < normal_Skills.Length; i++)
                {
                    saveSkillUnlocks.isUnlocked[i] = normal_Skills[i].Unlocked;
                }
                SaveSystem.SaveSkills(saveSkillUnlocks);
            }

            currScore.ScoreAmount -= Price;
            SaveSystem.SaveScore(currScore);
            BuyText.SetText("Bought!");
            normal_Skills[4].Locked = false;
        }
    }

    public void SetTitle(string Title)
    {
        //title.SetText(Skillchosen);
        title.SetText(Title);
    }
    public void SetDescription(string Description)
    {
        description.SetText(Description);
        price.SetText("Cost: " + Price.ToString());
        
    }
    public void SetPrice(int PriceToSet)
    {
        Price = PriceToSet;
    }



   







    public void getScore()
    {
        currScore.ScoreAmount += 10000;
        SaveSystem.SaveScore(currScore);
    }


    public void DontLock()
    {
        if (normal_Skills[0].Unlocked)
        {
            normal_Skills[1].Locked = false;
            normal_Skills[2].Locked = false;
        }
      //  if (normal_Skills[1].Unlocked)
        //{
          //  normal_Skills[3].Locked = false;
        //}
        if (normal_Skills[2].Unlocked)
        {
            normal_Skills[4].Locked = false;
        }
    }
}

public class Skill
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Locked { get; set; }
    public int Price { get; set; }
    public bool Unlocked {  get; set; }

}

public class SkillToSave
{
    public bool[] isUnlocked;
}
