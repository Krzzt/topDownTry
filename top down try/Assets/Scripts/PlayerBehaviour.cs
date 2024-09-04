using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using Unity.VisualScripting;

public class PlayerBehaviour : MonoBehaviour
{
    public float currentExp;
    public double maxExp = 10;
    public int currentLevel = 1;
    public float damageTick = 0.2f;
    public float lastDamageTick = 0f;
    public float aValue = 1f;
    private GameObject Dark;
    private GameObject DarkCanvas;
    private CanvasGroup trans;
    private CanvasGroup transCanvas;
    public GameObject Dark_Effect;
    public GameObject Upgrades;
    private Upgrades upgradeScript;
    public float chancetocrit;

    public Upgrades upgradescript;

    public int RegenRate = 0;
    public float RegenTimer = 5;
    public int thornDamage;
    public bool thorns = false;
    public float thornDamageTimer = 0.5f;
    public float thornDamageTicks = 0.5f;
    public bool thornrefresh;

    public bool knockback = false;
    public int knockbackStack = 0;

    public int takeDamage = 5;

    public bool doubleshot = false;

    public float overflowExp;

    public int scoreToSave;



    [SerializeField] Healthbar _healthbar;
    [SerializeField] ExpBar _expBar;
    [SerializeField] ExperienceManager _expManager;
    [SerializeField] public Weapon Weapon;

    public GameObject Gamemanager;
    public GameManager ScoreGiver;
    

    public GameObject Spawner;
    [SerializeField] Spawner spawner;

    public UnitHealth _playerHealth = new UnitHealth(100, 100);



    public TMP_Text ExpTrack;
    public TMP_Text HealthTrack;
    public TMP_Text Lvl;

    public TMP_Text Regen_text;


    public float upgradetimer = 1.5f;
    public bool uptimer = false;

    public GameObject Dead;
    private bool deadscore = false;
    public TMP_Text Scoreend;
    public int allScore;
    public bool once = false;

    private SaveScore currScore;


    private void Awake()
    {
        currScore = new SaveScore { };
        SaveSystem.LoadScore(currScore);
    }
    void Start()
    {
        Dark = GameObject.FindWithTag("Dark");
        DarkCanvas = GameObject.FindWithTag("DarkCanvas");
        Spawner = GameObject.FindWithTag("WaveSpawner");
        spawner = Spawner.GetComponent<Spawner>();
        _expBar.SetMaxExp((float) maxExp);
        upgradescript = Upgrades.GetComponent<Upgrades>();
        Dead.SetActive(false);
        Gamemanager = GameObject.FindWithTag("GameManager");
        ScoreGiver = Gamemanager.GetComponent<GameManager>();
    

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "EnemyBullet" && Time.time >= lastDamageTick +0.05f)
        {
            lastDamageTick = Time.time;
            
            EnemyBullet bullethit = collision.gameObject.GetComponent<EnemyBullet>();
            UnityEngine.Debug.Log(bullethit.damage);
            PlayerTakeDamage((int)bullethit.damage);
            
        }

    }
   

    private void OnEnable()
    {
        _expManager.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        _expManager.OnExperienceChange -= HandleExperienceChange;
    }
    private void HandleExperienceChange(float newExperience)
    {
        currentExp += newExperience;
        if (currentExp >= maxExp) 
        {
            if (spawner.currWave % 10 != 0)
            {
                _expBar.SetMaxExp((float)maxExp);
                LevelUp();
            }
            else
            {
                currentExp = (float)maxExp - 1f;
            }
        }
    }

    public void Buttonlvlup()
    {
        LevelUp();
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExp = overflowExp;
        
        maxExp = (int)(maxExp * 1.25f);
        Time.timeScale = 0;
        Instantiate(Dark_Effect, new Vector3(0, 0, 0), Quaternion.identity);
        setCanvas();
        Opacity();
        UpgradeButtons(false);
       
    }

    public void UpgradeScreenBossKill()
    {
      
            Time.timeScale = 0;
            Instantiate(Dark_Effect, new Vector3(0, 0, 0), Quaternion.identity);
            setCanvas();
            Opacity();
            UpgradeButtons(true);
        
      
   
       
    }
   
    public void Opacity()
    {
        transCanvas.alpha = 0.75f;
        trans.alpha = 0.75f;

    }

    public void setCanvas()
    {

        DarkCanvas = GameObject.FindGameObjectWithTag("DarkCanvas");
        Dark = GameObject.FindGameObjectWithTag("Dark");

        transCanvas = DarkCanvas.GetComponent<CanvasGroup>();
        trans = Dark.GetComponent<CanvasGroup>();
    }

    public void UpgradeButtons(bool bossKill)
    {
        
        Instantiate(Upgrades, new Vector3(552, 239, 0), Quaternion.identity);
        Upgrades.SetActive(true);
        upgradeScript = Upgrades.GetComponent<Upgrades>();
        upgradeScript.ButtonsSet(bossKill);
        
        
        
    }
 
    void Update()
    {
       
        if (currentExp < 0) 
        {
            currentExp = 0;
        }
        Gamemanager = GameObject.FindWithTag("GameManager");
        ScoreGiver = Gamemanager.GetComponent<GameManager>();

        if (deadscore)
        {
            for (int i = 0; i <= ScoreGiver.Score; i++)
            {
                Scoreend.SetText("Score: " + i);
            }
            
        }
       
        takeDamage = (int)(5 * (spawner.currWave / 10));
        if (_playerHealth._currentHealth <= 0 && !once)
        {
            Death();
            once = true;
            
           
        }

        chancetocrit = UnityEngine.Random.Range(0f, 100f);

        if (thornDamageTimer > 0 && thorns)
        {
            thornDamageTimer -= Time.deltaTime;
        }
        if (thornDamageTimer <= 0 && thorns)
        {
            thornDamageTimer = thornDamageTicks;
            thornrefresh = true;
        }


        Regen_text.SetText("Regen: " + RegenRate);

        Lvl.SetText("Lvl: " + currentLevel);
        ExpTrack.SetText(currentExp.ToString("F1") + "/" + maxExp.ToString());
        _expBar.SetExp((float)currentExp);
        _expBar.SetMaxExp((float) maxExp);
        HealthTrack.SetText(_playerHealth._currentHealth.ToString() + "/" + _playerHealth._currentMaxHealth.ToString());
        _healthbar.SetMaxHealth((int)_playerHealth._currentMaxHealth);
        //Debug.Log(_playerHealth._currentMaxHealth);
        _healthbar.SetHealth(_playerHealth.Health);

        RegenTimer -= Time.deltaTime;

        if (RegenTimer <= 0)
        {
            _playerHealth.HealUnit(RegenRate);
            RegenTimer = 5;
        }



    }

    public void PlayerTakeDamage(int dmg) 
    {
        _playerHealth.DamageUnit(dmg);
        _healthbar.SetHealth(_playerHealth.Health);
    }

    public void PlayerHeal(int healing)
    {
        _playerHealth.HealUnit(healing);
        _healthbar.SetHealth(_playerHealth.Health);
    }

    public void Regen(int amount)
    {
        RegenRate += amount;

    }

    public void IncreaseThornDamage(int amount)
    {
        thornDamage += amount;
    }

    public int GetMaxHealth()
    {
        return _playerHealth.MaxHealth;
    }


    public void Death()
    {
        Dead.SetActive(true);
        deadscore = true;
        currScore.ScoreAmount += ScoreGiver.Score;
        SaveSystem.SaveScore(currScore);
        Time.timeScale = 0;
    }

 
 
}

public class SaveScore : MonoBehaviour
{
    public int ScoreAmount;
}
