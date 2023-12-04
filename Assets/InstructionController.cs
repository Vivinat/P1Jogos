using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionController : MonoBehaviour
{
    [SerializeField] private Blueprint blueprint;

    [SerializeField] private TextMeshProUGUI quotaText;
    
    //Start is called before the first frame update
    void Start()
    {
        quotaText.text = "Cota: " + blueprint.CoinQuota;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
