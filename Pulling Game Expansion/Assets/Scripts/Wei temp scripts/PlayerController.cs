using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationDirection
{
    Front,
    Right,
    Back,
    Left
}

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpVelocity;
    private Rigidbody2D _rb;
    public Vector2 movement;

    //public KeyCode jumpKey;

    public Animator playerAnimator;
    private string _currentState;
    private AnimationDirection _playerDirection;

    #region Animation Clips
    /*public string IDLE_FRONT; //merry's method, attentioncatching and discerns it as animation
    public string IDLE_RIGHT;
    public string IDLE_BACK;
    public string IDLE_LEFT;*/
    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");


    }

    //runs at a set speed regardless of framerate
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement * movementSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("jump");
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Force);
        }

        #region anim
        /*
        //check to make sure it doesn't override your animation states 
        if (_playerDirection == AnimationDirection.Front)
            ChangeAnimationState(IDLE_FRONT);
        else if (_playerDirection == AnimationDirection.Right)
            ChangeAnimationState(IDLE_RIGHT);
        else if (_playerDirection == AnimationDirection.Back)
            ChangeAnimationState(IDLE_BACK);
        else if (_playerDirection == AnimationDirection.Left)
            ChangeAnimationState(IDLE_LEFT);

        //edit sprite
        if (movement.x > 0) //right
        {
            ChangeAnimationState(IDLE_RIGHT);
            _playerDirection = AnimationDirection.Right; 
        }
        else if(movement.x < 0)
        {
            ChangeAnimationState(IDLE_LEFT);
            _playerDirection = AnimationDirection.Left;
        }
        else if(movement.y < 0)
        {
            ChangeAnimationState(IDLE_FRONT);
            _playerDirection = AnimationDirection.Front;
        }
        else if(movement.y > 0)
        {
            ChangeAnimationState(IDLE_BACK);
            _playerDirection = AnimationDirection.Back;
        }
        */
        #endregion
    }


    /* void ChangeAnimationState(string newState)
     {
         if (_currentState == newState) return;
         playerAnimator.Play(newState);

         _currentState = newState; 
     }*/


}