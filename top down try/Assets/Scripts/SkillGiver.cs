using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGiver : MonoBehaviour
{
    public int[] Skills = new int[200];
    public bool[] SkillBools = new bool[200];
    public SkillToSave skillSaver = new SkillToSave();



    [SerializeField] private Weapon weapon;
    private GameObject Weapon;
    private GameObject player;
    private PlayerBehaviour playerBehaviour;
    private void Awake()
    {

        SaveSystem.LoadSkills(skillSaver);

        for (int i = 0; i < skillSaver.isUnlocked.Length; i++)
        {
            SkillBools[i] = skillSaver.isUnlocked[i];
        }





    }

    public void Start()
    {
        Weapon = GameObject.FindWithTag("Weapon");
        weapon = Weapon.GetComponent<Weapon>();
        player = GameObject.FindWithTag("Player");
        playerBehaviour = player.GetComponent<PlayerBehaviour>();   
        GiveUpgrades();
    }
    public void GiveUpgrades()
    {
       
        if (SkillBools[0])
        {
            weapon.moreGrenades();
        }
        if (SkillBools[1])
        {
            playerBehaviour._playerHealth.addmaxHealth(15);
        }
        if (SkillBools[2])
        {
            playerBehaviour.Regen(1);
        }
    }
}
