using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerBehaviour player;
    [SerializeField] private Bullet bullet;
    private GameObject Bullet;
 
    public int Score;
    
    public float timer = 60;
    public TMP_Text Scoretext;
    private GameObject Spawner;
    private Spawner spawnerscript;
    public GameObject Dead;
    public static GameManager gameManager { get; private set; }

    public bool CandyEnabled;


    

    void Awake()
    {
        Dead = GameObject.FindWithTag("Death");
        Dead.SetActive(false);
        player.once = false;

        if (gameManager != null && gameManager != this) 
        {
            Destroy(this);  
        }
        else
        {
            gameManager = this;
        }

       Bullet = GameAssets.i.Bullet;
        bullet = Bullet.GetComponent<Bullet>();

        bullet.pierce = 1;
        bullet.pierceremaining = 1;
        Bullet.transform.localScale = new Vector3(2, 2, 1);

        Spawner = GameObject.FindWithTag("WaveSpawner");
        spawnerscript = Spawner.GetComponent<Spawner>();
        Score = 0;
        CandyEnabled = false;

        
    }
    private void Update()
    {
        Scoretext.SetText("Score: " + Score);

        
        
       
        
       
       
    }
    public void addscore(int amount)
    {
        Score += amount;
    }
}
