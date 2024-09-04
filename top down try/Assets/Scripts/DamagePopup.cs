using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using CodeMonkey.Utils;
using UnityEngine.UIElements;

public class DamagePopup : MonoBehaviour
{
    
    private TextMeshPro textmesh;
    private float disappearTimer;
    private Color textColor;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private Vector3 moveVector;
    public Transform pfDamagePopup;
    private void Awake()
    {
      textmesh = transform.GetComponent<TextMeshPro>();
        textColor = UtilsClass.GetColorFromString("ffffff");
    }

    public static DamagePopup Create( int damageAmount, bool isCrit, Vector3 position)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCrit, position);
        return damagePopup;
        

    }
    public void Setup (int damageAmount, bool isCrit, Vector3 position)
    {
        textmesh.SetText(damageAmount.ToString());
        disappearTimer = DISAPPEAR_TIMER_MAX;
        
        if (isCrit)
        {
            textmesh.fontSize = 350;
            textColor = UtilsClass.GetColorFromString("f21f1f");
        }
        else
        {
            textmesh.fontSize = 250;
            textColor = UtilsClass.GetColorFromString("ffffff");
        }
        textmesh.color = textColor;

        moveVector = new Vector3(.7f, 1) * 60f;

        

    }

    private void Update()
    {


        
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * Time.deltaTime * 8f;


        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textmesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.7f) 
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
    }
}
