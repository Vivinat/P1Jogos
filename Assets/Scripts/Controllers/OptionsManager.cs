using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeText;
    
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            volumeText.text = PlayerPrefs.GetFloat("Volume").ToString("0.0");
        }
    }
    
    private void Start()
    {
        volumeSlider.onValueChanged.AddListener((v) =>
        {
            volumeText.text = v.ToString("0.0");
            PlayerPrefs.SetFloat("Volume", v);
            AudioManager.instance.ChangeSoundVolume("Menu", v);
            AudioManager.instance.ChangeSoundVolume("Day", v);
            AudioManager.instance.ChangeSoundVolume("Night", v);
            AudioManager.instance.ChangeSoundVolume("Victory",v);
        });
    }

    public void ReturnToMenu()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Menu");
    }
}