using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opacity : MonoBehaviour
{
    public float aValue = 0;
    private CanvasGroup trans;
    void Start()
    {
        trans = GetComponent<CanvasGroup>();
        trans.alpha = aValue;
    }
}
