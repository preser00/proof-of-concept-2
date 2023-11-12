using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f; //How fast can we walk
    public float jumpSpeed = 3f; //How high can we jump
    public bool groundCheck; //Is the frog landed
    public bool isSwinging; //Is the frog Swinging
    public Vector2 ropeHook;
    private SpriteRenderer _playerSprite;
    private Rigidbody2D _rb;
    private bool _isJumping; //Is the frog in the middle of Jumping
    private Animator _animator; //Reserved for animation down the line
    private float _jumpInput;
    private float _horizontalInput;


    void Awake()
    {
        //Assigning variables with their proper target
        _playerSprite = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        /* _animator = GetComponent<Animator>(); */
    }

    void Update()
    {
        _jumpInput = Input.GetAxis("Jump");
        _horizontalInput = Input.GetAxis("Horizontal");
        var halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);
    }

    void FixedUpdate()
    {
        if (_horizontalInput < 0f || _horizontalInput > 0f)
        {
            /* _animator.SetFloat("Speed", Mathf.Abs(_horizontalInput)); */
            _playerSprite.flipX = _horizontalInput < 0f;

            if (groundCheck)
            {
                var groundForce = speed * 2f;
                _rb.AddForce(new Vector2((_horizontalInput * groundForce - _rb.velocity.x) * groundForce, 0));
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
            }
        }
        /*else
        {
            _animator.SetFloat("Speed", 0f); 
        } */

        if (!groundCheck) return;

        _isJumping = _jumpInput > 0f;
        if (_isJumping)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
        }
    }
}
