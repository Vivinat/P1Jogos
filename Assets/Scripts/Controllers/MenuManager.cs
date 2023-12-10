using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            AudioManager.instance.StopSound("Victory");
            AudioManager.instance.PlaySound("Menu");
        }

        if (SceneManager.GetActiveScene().name == "Ending")
        {
            AudioManager.instance.StopSound("Night");
            AudioManager.instance.PlaySound("Victory");
        }
    }

    public void CallMainMenu()
    {
        SceneManager.LoadScene("Menu");
        AudioManager.instance.StopSound("Day");
        AudioManager.instance.StopSound("Night");
    }

    public void CallTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("NewHero");
    }

    public void CallOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToGame()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerInput>().ActivateInput();
        GameObject options = GameObject.FindWithTag("Options");
        options.SetActive(false);
    }
}
