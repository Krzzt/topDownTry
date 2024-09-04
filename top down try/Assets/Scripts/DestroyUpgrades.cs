using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class DestroyUpgrades : MonoBehaviour
{
    private GameObject UpgradeCanvas;
    private GameObject DarkScreen;
    private GameObject Player;
    private PlayerBehaviour PlayerBehaviour;
    public GameObject GameManager;
    public PauseMenu pause;
   
    public Upgrades UpgradeScript;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");  
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();
        GameManager = GameObject.FindWithTag("GameManager");
        pause = GameManager.GetComponent<PauseMenu>();
        UpgradeCanvas = GameObject.FindWithTag("UpgradeCanvas");
        UpgradeScript = UpgradeCanvas.GetComponent<Upgrades>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void destroyupgrade()
    {
           
        UpgradeCanvas = GameObject.FindWithTag("UpgradeCanvas");
        Destroy(UpgradeCanvas);
        
        DarkScreen = GameObject.FindGameObjectWithTag("DarkFather");
        Destroy(DarkScreen);
        if (!pause.isPaused)
        {
            Time.timeScale = 1f;
        }
       


    }


}
