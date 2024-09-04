using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using CharlieMadeAThing.NeatoTags.Core;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject Continuebutton;
    private GameObject Destroybutton;
    

    public List<Enemy> enemies = new List<Enemy>();
    public float currWave;
    
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLocation;
    public int spawnIndex;

    private bool once = true;
    private float timepassed = 2f;
    private float timepass;

    public GameObject NextWaveButton;
    private GameObject CurrNextWaveButton;

    public int waveDuration;
    private float waveTimer;
    private double spawnInterval;
    private double spawnTimer;

    private float autoNextWaveTimer = 60f;

    [SerializeField] public NeatoTag Enemy;

    GameObject[] allObjects;

    GameObject[] enemycount;

    public float EnemyHealthMultiplier = 1.0f;
    public float welovefloat;

    public GameObject Player;
    public PlayerBehaviour PlayerBehaviour;

    public GameObject Expmanager;
    public ExperienceManager expmanage;


    public TMP_Text timer;
    public TMP_Text Wavetracker;
    public TMP_Text enemycounter;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public GameObject Boss;

    private GameObject GameManager;
    private GameManager gamemanagerscript;

    
    // Start is called before the first frame update

    private void Awake()
    {
        welovefloat = (float)((float)currWave / 10);
        EnemyHealthMultiplier = 1 + welovefloat;
        Player = GameObject.FindWithTag("Player");
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();
        Expmanager = GameObject.FindWithTag("Exp");
        expmanage = Expmanager.GetComponent<ExperienceManager>();
        GameManager = GameObject.FindWithTag("GameManager");
        gamemanagerscript = GameManager.GetComponent<GameManager>();
    }
    void Start()
    {
        GenerateWave();
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        //Buttonposition = Continuebutton.GetComponent<Transform>();


    }



    // Update is called once per frame
    void FixedUpdate()
    {
        enemycount = GameObject.FindGameObjectsWithTag("Enemy");
        autoNextWaveTimer -= Time.fixedDeltaTime;

        if (autoNextWaveTimer <= 0 && currWave % 10 != 0 && (currWave + 1) % 10 != 0)
        {
            Destroythebutton();
            NextWave();
            autoNextWaveTimer = 60f;
           
        }

      
       

        timer.SetText(autoNextWaveTimer.ToString("F0"));
        Wavetracker.SetText("Wave " + currWave.ToString());
        enemycounter.SetText("Enemies: " + enemycount.Length.ToString());

        if (waveTimer >= 0)
        {
            waveTimer -= Time.fixedDeltaTime;
        }

        spawnTimer -= Time.fixedDeltaTime;
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {

                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position, Quaternion.identity); // spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0); // and remove it
                for (int i = 1; i < enemiesToSpawn.Count; i++)
                {
                    enemy.name = "enemy " + i;
                }
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;

                if (spawnIndex + 1 <= spawnLocation.Length - 1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }

        }
        if (waveTimer <= 0)
        {
            waveTimer = 0;

        }

        if (waveTimer == 0 && once && enemycount.Length <= 75)

        {
            if (currWave % 10 == 0 || (currWave+1) % 10 == 0)
            {
            
            }
            else
            {
                //Instantiate(Continuebutton, new Vector3(552, 269, 0), Quaternion.identity);
                //Buttonposition.position = new Vector3(1730, 35, 0);
                Instantiate(NextWaveButton, new Vector3(552, 265, 0), Quaternion.identity);
                once = false;
               
                GameObject startnextwavebuttonCanvas = GameObject.FindWithTag("NextWave");
                GameObject Startnextwavebutton = startnextwavebuttonCanvas.transform.GetChild(0).gameObject;
               Button nextwavebuttonbutton = Startnextwavebutton.GetComponent<Button>();
                nextwavebuttonbutton.onClick.AddListener(delegate{gameObject.GetComponent<Spawner>().NextWave(); });
                
            }
         
            if (enemycount.Length <= 0)
            {
                Destroythebutton();
            }
            
        }
        if (enemycount.Length <= 0)
        {
            timepass += Time.deltaTime;
            if (timepass >=  timepassed && waveTimer <= 0 )
            {
                
                    NextWave();
                   
                

            }


        }
    }

    
    public void NextWave()
    {
        Expgiver();
       
        timepass = 0;
        autoNextWaveTimer = 60f;
      
        
        
        currWave++;
        if (currWave % 10 == 0)
        {
            GenerateBoss();
        }
        else if (currWave % 10 != 0)
        {
            GenerateWave();
        }
       
        once = true;
        Destroythebutton();
    
      
        welovefloat = (float)((float)currWave / 10);
        EnemyHealthMultiplier = 1 + welovefloat;
    

    }

    public void Expgiver()
    {
       


            float expgive = (currWave+1) * autoNextWaveTimer / 5;
            gamemanagerscript.addscore((int)expgive);
            


        
    }

    public void Destroythebutton()
    {
        //Buttonposition.position = new Vector3(218, -59, 0);
        CurrNextWaveButton = GameObject.FindWithTag("NextWave");
        Destroy(CurrNextWaveButton);
    }
    public void GenerateWave()
    {
        waveValue = (int)currWave * 10;
        GenerateEnemies();

        spawnInterval = (float) waveDuration / enemiesToSpawn.Count; 
        waveTimer = waveDuration; 
    }

    public void GenerateBoss()
    {
        waveValue = (int)currWave * 10;
        GameObject boss = Instantiate(Boss, spawnLocation[1].position, Quaternion.identity);
        boss.name = "Boss";
        waveTimer = waveDuration;

    }

    public void GenerateEnemies()
    {


        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId;
            if (currWave <= 4)
            {
                 randEnemyId = Random.Range(0, 2);
            }
            else if (currWave >= 5 && currWave <= 10)
            {
                randEnemyId = Random.Range(0, 4);
            }
            else if (currWave > 10 && currWave <= 20)
            {
                randEnemyId = Random.Range(1, 5);
             
                    
                
            }
            else if (currWave > 20)
            {
                randEnemyId= Random.Range(2, 6);
            }
            else
            {
                randEnemyId = Random.Range(1, 5);
            }
             
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
            else if (waveValue - randEnemyCost < 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
                waveValue = 0;
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    public int getWaveValue()
    {
        return waveValue;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}

