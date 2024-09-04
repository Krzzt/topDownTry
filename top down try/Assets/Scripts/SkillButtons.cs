using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillButtons : MonoBehaviour
{
    
    public GameObject skills;
   private Skills skillscript;
    public int id;
    public Button thisButton;
    // Start is called before the first frame update
    public void Awake()
    {
        skillscript = skills.GetComponent<Skills>();
        thisButton = gameObject.GetComponent<Button>();
    
    }
    private void Start()
    {
        if (skillscript.normal_Skills[id].Locked && !skillscript.normal_Skills[id].Unlocked)
        {
            thisButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectSkill()
    {
        skillscript.SetPrice(skillscript.normal_Skills[id].Price);
        skillscript.SetTitle(skillscript.normal_Skills[id].Name);
        skillscript.SetDescription(skillscript.normal_Skills[id].Description);
        skillscript.Skillchosen = skillscript.normal_Skills[id].Name;
    
        if (skillscript.normal_Skills[id].Unlocked)
        {
            skillscript.BuyText.SetText("Bought!");
        }
        else if (skillscript.normal_Skills[id].Locked && !skillscript.normal_Skills[id].Unlocked)
        {
            thisButton.interactable = false;
            skillscript.BuyText.SetText("Locked!");
        }
        else if (!skillscript.normal_Skills[id].Locked && !skillscript.normal_Skills[id].Unlocked)
        {
            skillscript.BuyText.SetText("Buy");
        }
    }
}
