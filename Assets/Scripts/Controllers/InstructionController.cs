using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstructionController : MonoBehaviour
{
    [SerializeField] private Blueprint blueprint;

    [SerializeField] private TextMeshProUGUI quotaText;

    private Player player;
    
    //Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player.GetComponent<PlayerInput>().DeactivateInput();
        quotaText.text = "Quota: " + blueprint.CoinQuota;
    }

    public void ClosePanel()
    {
        player.GetComponent<PlayerInput>().ActivateInput();
        gameObject.SetActive(false);
    }
}
