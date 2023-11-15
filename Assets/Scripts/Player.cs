using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class Player : MonoBehaviour
{
    //Componentes
    Animator _playerAnimator;
    Rigidbody2D _playerRb;
    BoxCollider2D _playerCollider;
    Vector2 mov;
    
    //Variaveis para Vcam poder focar o jogador
    [SerializeField] private CinemachineVirtualCamera vcam;
    private Transform finishLine;
    private bool isFocusing = false;
    
    //Variavel para gravidade
    private bool isGravitating = false;
    
    //internas
    [SerializeField] float playerSpeed = 5;
    [SerializeField] float playerJump;
    [SerializeField] private GameObject platform;
    public int buildQuant;
    bool canDoubleJump = false;
    public int doubleJumpQuant;
    private bool isTakingDamage;

    //HUD
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI coinText;
    public int coinQuant;
    
    //Outros scripts
    [SerializeField] private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        finishLine = GameObject.FindWithTag("FinishLine").transform;
        audioManager = FindObjectOfType<AudioManager>();
       _playerRb = GetComponent<Rigidbody2D>();
       _playerAnimator = GetComponent<Animator>();
       _playerCollider = GetComponent<BoxCollider2D>();
       jumpText.text = ("Pulos: " + doubleJumpQuant + '\n' + "Plataformas: " + buildQuant);
       coinText.text = ("Moedas: " + coinQuant);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTakingDamage)
        {
            Movimentacao();            
        }

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
                audioManager.PlaySound("DoubleJump");
                canDoubleJump = true;
            }
            else if (canDoubleJump && doubleJumpQuant > 0)
            {
                _playerRb.velocity = new Vector2(_playerRb.velocity.x, playerJump);
                canDoubleJump = false;
                audioManager.PlaySound("Spend");
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
            audioManager.PlaySound("Recover");
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
            audioManager.PlaySound("Build");
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
        if (isFocusing == false)
        {
            audioManager.PlaySound("Focus");
            Debug.Log("Em foco finish");
            vcam.Follow = finishLine.transform;
            isFocusing = true;
            return;
        }
        Debug.Log("Em foco player");
        isFocusing = false;
        vcam.Follow = this.transform;
    }

    private void OnRestart(InputValue inputValue)
    {
        GetComponent<PlayerInput>().DeactivateInput();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnGravity(InputValue inputValue)
    {
        if (isGravitating == false)
        {
            audioManager.PlaySound("Gravity");
            Debug.Log("Gravitando");
            isGravitating = true;
            _playerRb.gravityScale = -0.5f;
            return;
        }
        Debug.Log("Removendo gravidade");
        isGravitating = false;
        _playerRb.gravityScale = 8;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(Waiter());
        }
    }
    
    IEnumerator Waiter()
    {
        isTakingDamage = true;
        _playerRb.velocity = new Vector2(-transform.localScale.x * 3, 10.0f);
        _playerAnimator.SetBool("IsDamage", true);
        yield return new WaitForSecondsRealtime(0.1f);
        _playerRb.velocity = Vector2.zero;
        GetComponent<PlayerInput>().DeactivateInput();
        Physics2D.IgnoreLayerCollision(8,10, true);
        yield return new WaitForSecondsRealtime(1.5f);
        _playerAnimator.SetBool("IsDamage", false);
        GetComponent<PlayerInput>().ActivateInput();
        Physics2D.IgnoreLayerCollision(8,10, false);
        isTakingDamage = false;
    }

}
