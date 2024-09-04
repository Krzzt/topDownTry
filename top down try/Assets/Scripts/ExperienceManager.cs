using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ExperienceManager : MonoBehaviour
{
    public float expmultiplier = 1;
    public static ExperienceManager Instance;

    public delegate void ExperienceChangeHandler(float amount);
    public event ExperienceChangeHandler OnExperienceChange;

    private GameObject Player;
    private PlayerBehaviour PlayerBehaviourScript;

    public TMP_Text expmult_text;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerBehaviourScript = Player.GetComponent<PlayerBehaviour>();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void AddExperience(float amount)
    {
        float exphandle = amount * expmultiplier;
        float expgive = exphandle;
        PlayerBehaviourScript.overflowExp = (expgive + PlayerBehaviourScript.currentExp) - (float)PlayerBehaviourScript.maxExp;
        OnExperienceChange?.Invoke(expgive);
        
       
    }

    public void addMultiplier(float amount)
    {
        expmultiplier += amount;
    }

    private void Update()
    {
        expmult_text.SetText("Exp Mult.: " + expmultiplier);
    }
}
