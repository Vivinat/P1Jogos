using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TimeOfDay
{
    Day,
    Night,
};

public class StageController : MonoBehaviour
{
    [SerializeField] private Blueprint blueprint;
    [SerializeField] private Player player;
    [SerializeField] private GameObject endPhaseSummary;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TimeOfDay timeOfDay;

    private string nextStageName;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (timeOfDay == TimeOfDay.Day && !audioManager.IsPlaying("Day"))
        {
            audioManager.StopSound("Menu");
            audioManager.PlaySound("Day");
            return;
        }

        if (timeOfDay == TimeOfDay.Night && !audioManager.IsPlaying("Night"))
        {
            audioManager.StopSound("Day");
            audioManager.PlaySound("Night");
            return;
        }
    }

    public void CallQuota()
    {
        endPhaseSummary.SetActive(true);
        if (player.coinQuant >= blueprint.CoinQuota)
        {
            endPhaseSummary.GetComponentInChildren<TextMeshProUGUI>().text = "Cota Batida!";
            Debug.Log("Jogador tem numero igual ou maior ao obrigatorio. Passou");
            nextStageName = blueprint.nextStage;
            return;
        }
        endPhaseSummary.GetComponentInChildren<TextMeshProUGUI>().text = "Fracasso...";
        nextStageName = SceneManager.GetActiveScene().name;
    }
    public void CallNextStage()
    {
        SceneManager.LoadScene(nextStageName);
    }
    
}
