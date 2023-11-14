using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    [SerializeField] private Blueprint blueprint;
    [SerializeField] private Player player;

    public void CallQuota()
    {
        if (player.coinQuant >= blueprint.CoinQuota)
        {
            Debug.Log("Jogador tem numero igual ou maior ao obrigatorio. Passou");
            //CallNextStage();
        }
        Debug.Log("Cota não foi batida");
    }
    
    
    public void CallNextStage()
    {
        SceneManager.LoadScene(blueprint.nextStage);
    }
    
}
