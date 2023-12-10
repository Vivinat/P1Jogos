using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameOptions : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Player player;
    
    public void CallInGameOptions()
    {
        player.GetComponent<PlayerInput>().DeactivateInput();
        if (optionsPanel.activeSelf)
        {
            return;
        }
        optionsPanel.SetActive(true);
    }
    
}
