using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ControllerInput _input;

    Rigidbody2D _rb;
    CapsuleCollider2D _capsuleCollider;

    float time;


  
    Vector2 MoveInput;

    [Header("PlayerMovementStats")]
    [SerializeField] float _HorizontalMovmentMultiplier;

    [Header("Jump Stats")]
    [SerializeField] float _initialJumpForceMultiplier;
    [SerializeField] float _coyoteTime;
    [SerializeField] float _coyoteTimeCounter;
    [SerializeField] bool _playerIsJumping;
    [SerializeField] AnimationCurve _jumpApexDecelCurve;

    [Header("GroundCheck")]
    [SerializeField] bool _playerIsGrounded;
    [SerializeField] float _groundCheckBuffer;
    [SerializeField] float _groundCheckRadiusMultiplier;
    RaycastHit2D GroundCheckRaycast;
    float _playerHeight;

    [Header("Gravity Timers")]
    [SerializeField] float _gravityFallCurrent = -100.0f;
    [SerializeField] float _gravityFallMin = -100.0f;
    [SerializeField] float _gravityFallMax = -500.0f;
    [SerializeField] float _gravityFallIncrementAmount = -20.0f;
    [SerializeField] float _gravityFallIncrementTime = 0.05f;
    [SerializeField] float _playerFallTimer = 0.0f;
    [SerializeField] float _gravityGrounded = -1.0f;
    [SerializeField] float _maxSlopeAngle = 47.5f;


    





    private void Awake()
    {

        _input = GetComponent<ControllerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
     

    }
    private void Start()
    {
        _playerHeight = _capsuleCollider.bounds.extents.y * 2;
    }
    private void Update()
    {
        time += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        
        MoveInput = GetMoveInput();
        MoveInput = PlayerMove();
        _playerIsGrounded = Grounded();
        MoveInput.y = PlayerFallGravity();
        MoveInput.y = PlayerBounce();
        MoveInput.y += BounceObject();



        MoveInput *= _rb.mass;
        _rb.AddForce(MoveInput, ForceMode2D.Impulse);

        // to prevent forcemode.impulse from accelerating to fast
        Mathf.Clamp(_rb.velocity.x, 0, 1);

    }

    Vector2 GetMoveInput()
    {
        return new Vector2(_input.moveInput.x, 0);
    }

    Vector2 PlayerMove()
    {
        return MoveInput * _HorizontalMovmentMultiplier;
    }

    bool Grounded()
    {
        float calculatedRadius = _capsuleCollider.bounds.extents.x * _groundCheckRadiusMultiplier;
        return GroundCheckRaycast = Physics2D.CircleCast(transform.position, calculatedRadius, Vector2.down, (_playerHeight/2) - calculatedRadius + _groundCheckBuffer);
        
        
    }

    private float PlayerFallGravity()
    {
        float gravity = MoveInput.y;
        if (_playerIsGrounded)
        {
            _gravityFallCurrent = _gravityFallMin;
        }
        else
        {
            _playerFallTimer -= Time.fixedDeltaTime;
            if (_playerFallTimer < 0.0f)
            {
                if (_gravityFallCurrent > _gravityFallMax)
                {
                    _gravityFallCurrent += _gravityFallIncrementAmount;
                }
                _playerFallTimer = _gravityFallIncrementTime;

            }
            gravity = _gravityFallCurrent;
        }
        return gravity;
    }

    private float PlayerBounce()
    {
        Vector2 Vel = _rb.velocity;
        Vel.y = 0.0f;
        float calculatedJumpInput = MoveInput.y;
        float jumpPeakDecelerationMultiplier;


        SetCoyoteTimeCounter();
        

        if (_playerIsGrounded)
        {
            
            _rb.velocity = Vel;
        }

        if (!_playerIsJumping && _coyoteTimeCounter > 0.0f && _playerIsGrounded)
        {
            jumpPeakDecelerationMultiplier = _jumpApexDecelCurve.Evaluate(time);
            calculatedJumpInput = _initialJumpForceMultiplier * jumpPeakDecelerationMultiplier;
            _playerIsJumping = true;
            _coyoteTimeCounter = 0.0f;
        }
        else if (_playerIsJumping && !_playerIsGrounded)
        {
            _playerIsJumping = false;

        }
        else if (_playerIsJumping && _playerIsGrounded)
        {
            _playerIsJumping = false;

        }
        return calculatedJumpInput;
    }

    private void SetCoyoteTimeCounter()
    {
        if (_playerIsGrounded)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.fixedDeltaTime;
        }
    }

    public float BounceObject()
    {
        if (GroundCheckRaycast.collider != null && GroundCheckRaycast.collider.tag == "BounceObject")
        {
            float bounceAmount = GroundCheckRaycast.collider.GetComponent<BounceObject>().bounceAmount;
            return bounceAmount;
        }
        else
        {
            return 0;
        }
        
    }

}
