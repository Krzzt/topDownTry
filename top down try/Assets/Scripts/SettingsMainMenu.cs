using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UpgradeMenu;
    public GameObject LoadoutMenu;
    void Awake()
    {
        gameObject.SetActive(false);
        UpgradeMenu.SetActive(false);
        LoadoutMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
