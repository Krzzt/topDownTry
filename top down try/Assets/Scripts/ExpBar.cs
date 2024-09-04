using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    Slider _expSlider;

    private void Start()
    {
        _expSlider = gameObject.GetComponent<Slider>();
    }



    public void SetMaxExp(float maxExp)
    {
        _expSlider.maxValue = maxExp;
        
    }

    public void SetExp(float exp)
    {
        _expSlider.value = exp;
        _expSlider.value = exp;
    }
}

