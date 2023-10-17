using System.Collections;
using System.Collections.Generic;
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

    //internas
    [SerializeField]
    float playerSpeed = 5;
    [SerializeField]
    float playerJump;    
    
    bool canDoubleJump = false;
    private int doubleJumpQuant = 5;

    public TextMeshProUGUI jumpText; 
    
    // Start is called before the first frame update
    void Start()
    {
       _playerRb = GetComponent<Rigidbody2D>();
       _playerAnimator = GetComponent<Animator>();
       _playerCollider = GetComponent<BoxCollider2D>();
       jumpText.text = ("Pulos: " + doubleJumpQuant);
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
                jumpText.text = ("Pulos " + doubleJumpQuant);
            }
        }
    }
}
