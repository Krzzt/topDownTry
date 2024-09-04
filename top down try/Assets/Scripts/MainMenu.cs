using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public bool isArcade = false;
  
    public CanvasGroup NormalScreen;
    public void PlayArcade()
    {
        SceneManager.LoadScene("ArcadeGame");
    }
    public void QuitGame() 
    {
        Application.Quit();
    }

    public void HoverArcade()
    {
        isArcade = true;
    }

    public void StopHoverArcade()
    {
        isArcade = false;
    }

    public void OpenUpgrades()
    {

    }
    private void Update()
    {
        if (isArcade && NormalScreen.alpha > 0)
        {
            NormalScreen.alpha -= 0.01f;
        }

        if (!isArcade && NormalScreen.alpha < 1)
        {
            NormalScreen.alpha += 0.01f;
        }
    }
}
