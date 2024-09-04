using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade_button : MonoBehaviour
{
    [SerializeField] private Upgrades Upgrades_script;
    public TMP_Text upgradetext;




    public void Upgrade()
    {
        
        string Upgrade_chosen = upgradetext.text;
        Upgrades_script.UpgradeChosen(Upgrade_chosen);
    }
}


