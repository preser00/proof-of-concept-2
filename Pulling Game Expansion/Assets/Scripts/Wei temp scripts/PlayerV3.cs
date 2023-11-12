using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/playlist?list=PLBIb_auVtBwBotxgdQXn2smO0Fvqqea4-
public class PlayerV3 : MonoBehaviour
{
    #region Movement Vars
    private Rigidbody2D _rb;

    public float speed;
    public float jumpForce;
    private float _inputHorizontal;
    private float _inputVertical;

    private bool _facingLeft = true;

    private bool _isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int _extraJumps;
    public int setExtraJumps; 

    #endregion

    private Animator _anim;
    private string _currentAnim;
    //private bool _isJumping = false; 

    private AudioSource _audioSource;
    public AudioClip jumpSfx;
    public AudioClip hurtSfx; 

    public string idle = "player idle";
    public string walk = "player walk"; 
    public string jump = "player jump";

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>(); 

        _extraJumps = setExtraJumps;

        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner"); 
        foreach (GameObject spawner in spawners) {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), spawner.GetComponent<Collider2D>(), true);
        }
    }

    private void FixedUpdate()
    {
        //for jumping
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); 

        //reg movement
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_inputHorizontal * speed, _rb.velocity.y);

        //sprite management
        if(_facingLeft == false && _inputHorizontal < 0){
            FlipSprite(); 
          } else if(_facingLeft == true && _inputHorizontal > 0 ){
            FlipSprite(); 
          }

        if (_isGrounded)
        {
            if (_inputHorizontal == 0)
            {
                ChangeAnimationState(idle);
            }
            else
            {
                ChangeAnimationState(walk);
                Debug.Log("walking"); 
            }
        }
        else
        {
            ChangeAnimationState(jump); 
        }
     
    }

    private void Update()
    {
        //jumping
        if (_isGrounded == true) {
            _extraJumps = setExtraJumps; 

        }

        if(Input.GetKeyDown(KeyCode.Z) && _extraJumps > 0)
        {
            _rb.velocity = Vector2.up * jumpForce;
            _extraJumps--;

            _audioSource.clip = jumpSfx;
            _audioSource.Play(); 
        } else if(Input.GetKeyDown(KeyCode.Z) && _extraJumps == 0 && _isGrounded == true)
        {
            _rb.velocity = Vector2.up * jumpForce;
            _audioSource.clip = jumpSfx;
            _audioSource.Play();

            ChangeAnimationState(jump);
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            _audioSource.clip = hurtSfx; 
            _audioSource.Play(); 

            if (GameManager.collected >= 3)
            {
                GameManager.collected -= 3;
            }
            else
            {
                GameManager.collected = 0; 
            }
            
        }
    }

    void FlipSprite()
    {
        _facingLeft = !_facingLeft;
        Vector3 Scaler = transform.localScale; //player's xyz scale values
        Scaler.x *= -1; //changes player's horizontal direction
        transform.localScale = Scaler; 
    }

    void ChangeAnimationState(string newAnim)
    {
        if (_currentAnim == newAnim) return;

        _anim.Play(newAnim);
        _currentAnim = newAnim; 
    }
}
