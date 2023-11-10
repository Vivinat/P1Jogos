using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utils;

public class Player : MonoBehaviour
{
    Animator _playerAnimator;
    Rigidbody2D _playerRb;
    BoxCollider2D _playerCollider;
    Vector2 mov;
    
    //Variaveis para Vcam poder focar o jogador
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] Transform finishLine;
    private bool finishInFocus = false;
    
    //internas
    [SerializeField] float playerSpeed = 5;
    [SerializeField] float playerJump;
    [SerializeField] private GameObject platform;
    public int buildQuant;
    bool canDoubleJump = false;
    public int doubleJumpQuant;

    //HUD
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI coinText;
    public int coinQuant;

    
    // Start is called before the first frame update
    void Start()
    {
        finishLine = GameObject.FindWithTag("FinishLine").transform;
       _playerRb = GetComponent<Rigidbody2D>();
       _playerAnimator = GetComponent<Animator>();
       _playerCollider = GetComponent<BoxCollider2D>();
       jumpText.text = ("Pulos: " + doubleJumpQuant + '\n' + "Plataformas: " + buildQuant);
       coinText.text = ("Moedas: " + coinQuant);
    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();
    }


    void Movimentacao()
    {
        _playerRb.velocity = new Vector2(mov.x*playerSpeed,_playerRb.velocity.y);

        bool IsRunning = Mathf.Abs(_playerRb.velocity.x) > Mathf.Epsilon;

        _playerAnimator.SetBool(Utils.Constants.IS_RUNNING,IsRunning);
           

        if(IsRunning)
            FlipSprite();
        
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(Mathf.Sign(_playerRb.velocity.x),1,1);
    }
   
    void OnMove(InputValue inputValue){
        mov = inputValue.Get<Vector2>();
    }
    
    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (_playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                _playerRb.velocity = new Vector2(_playerRb.velocity.x, playerJump);
                canDoubleJump = true;
            }
            else if (canDoubleJump && doubleJumpQuant > 0)
            {
                _playerRb.velocity = new Vector2(_playerRb.velocity.x, playerJump);
                canDoubleJump = false;
                doubleJumpQuant--;
                jumpText.text = ("Pulos: " + doubleJumpQuant + '\n' + "Plataformas: " + buildQuant);
            }
        }
    }

    public void AddColectable(string type)
    {
        if (type == "coin")
        {
            coinQuant++;
            coinText.text = ("Moedas: " + coinQuant);    
        }

        if (type == "block")
        {
            buildQuant++;
            jumpText.text = ("Pulos: " + doubleJumpQuant + '\n' + "Plataformas: " + buildQuant);
        }

    }

    void OnBuy(InputValue inputValue)
    {
        if (coinQuant > 0)
        {
            doubleJumpQuant += 1;
            coinQuant -= 1;
            coinText.text = ("Moedas: " + coinQuant);
            jumpText.text = ("Pulos: " + doubleJumpQuant + '\n' + "Plataformas: " + buildQuant);
            Debug.Log("Comprou");
        }
        else
        {
            Debug.Log("Sem grana chefe");    
        }
    }
    
    void OnBuild(InputValue inputValue)
    {
        if (buildQuant > 0)
        {
            buildQuant -= 1;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 1; 
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Instantiate(platform, worldPosition, Quaternion.identity);
            jumpText.text = ("Pulos: " + doubleJumpQuant + '\n' + "Plataformas: " + buildQuant);
        }
        else
        {
            Debug.Log("Sem grana chefe");    
        }
    }

    void OnFocus(InputValue inputValue)
    {
        if (finishInFocus == false)
        {
            Debug.Log("Em foco finish");
            vcam.Follow = finishLine.transform;
            finishInFocus = true;
            return;
        }
        Debug.Log("Em foco player");
        finishInFocus = false;
        vcam.Follow = this.transform;
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5);
    }

}
