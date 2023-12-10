using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BlueprintController : MonoBehaviour
{
    [SerializeField] private Blueprint blueprint;
    [SerializeField] private Player player;
    [SerializeField] public TextMeshProUGUI jumpQuant;
    [SerializeField] public TextMeshProUGUI platformQuant;
    [SerializeField] public TextMeshProUGUI quotaQuant;

    public void Awake()
    {
        Debug.Log("Aplicando blueprint");
        player.buildQuant = blueprint.BuildNumber;
        player.doubleJumpQuant = blueprint.JumpQuant;
        quotaQuant.text = ("Quota: " + blueprint.CoinQuota);
    }
}
