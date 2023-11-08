using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "Blueprint")]
public class Blueprint : ScriptableObject
{
    public int BuildNumber;
    public int CoinQuota;
    public int JumpQuant;
    public string nextStage;
}
    

