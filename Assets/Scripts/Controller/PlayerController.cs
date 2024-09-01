using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{   
    ControllerInput _input;
    Rigidbody2D _rb;
    CapsuleCollider2D _collider;

    public float _horizontalMultiplier;
    public float _jumpForce;

    public bool _isGrounded;
    public bool _isJumping;

    [Header("Gravity Timers")]
    [SerializeField] float _gravityFallCurrent = -100.0f;
    [SerializeField] float _gravityFallMin = -100.0f;
    [SerializeField] float _gravityFallMax = -500.0f;
    [SerializeField] float _gravityFallIncrementAmount = -20.0f;
    [SerializeField] float _gravityFallIncrementTime = 0.05f;
    [SerializeField] float _playerFallTimer = 0.0f;

    RaycastHit2D _hit;

    Vector2 _moveInput;
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<ControllerInput>();
        _collider = GetComponent<CapsuleCollider2D>();  
    }

    private void FixedUpdate()
    {
        _moveInput = GetMoveInput();
        _moveInput = PlayerMove();
        _isGrounded = Grounded();
       
        _rb.velocity =  new Vector2 (_moveInput.x, _rb.velocity.y);

        _rb.AddForce(Vector2.down * PlayerFallGravity(), ForceMode2D.Force);

    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
        }
    }


    Vector2 GetMoveInput()
    {
        return new Vector2(_input.moveInput.x, 0);
    }

    Vector2 PlayerMove()
    {
        return _moveInput * _horizontalMultiplier;
    }

    bool Grounded()
    {
        float castRadius = _collider.bounds.extents.x;
        float castDist = _collider.bounds.extents.y - castRadius;
        return _hit = Physics2D.CircleCast(transform.position, castRadius, Vector2.down, castDist);
    }

    private float PlayerFallGravity()
    {
        float gravity = _moveInput.y;
        if (_isGrounded)
        {
            _gravityFallCurrent = _gravityFallMin;
        }
        else
        {
            _playerFallTimer -= Time.fixedDeltaTime;
            if (_playerFallTimer < 0.0f)
            {
                if (_gravityFallCurrent < _gravityFallMax)
                {
                    _gravityFallCurrent += _gravityFallIncrementAmount;
                }
                _playerFallTimer = _gravityFallIncrementTime;

            }
            gravity = _gravityFallCurrent;
        }
        return gravity;
    }

}
