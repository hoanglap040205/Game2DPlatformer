using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private AnimationManager anim;

    private float inputValue;
    private float timeState;
    [SerializeField] private float speed;
    [SerializeField] private bool isGround;


    [Header("Rotate")]
    private float directionRight;
    private bool isRight;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isRight = true;
        directionRight = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateControler(inputValue);

        Jump();
        Crounching();


        anim.animFloat(anim.yVelocity, rb.velocity.y);
        anim.animFloat(anim.xVelocity, Mathf.Abs(inputValue));
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputValue * speed, rb.velocity.y);
    }

    private void Movement()
    {
        inputValue = Input.GetAxis("Horizontal");
        if (inputValue != 0 && isGround)
        {
            anim.changeState(AnimationState.RUN);
            inputValue /= 2f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                inputValue *= 2;
            }
        }
        else
        {
            anim.changeState(AnimationState.IDLE);
        }
    }

    private void Crounching()
    {
        if (Input.GetKey(KeyCode.S))
        {
            anim.changeState(AnimationState.CROUNCH);
            inputValue = 0;
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            anim.changeState(AnimationState.JUMP);
            isGround = false;
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }

    private void RotateControler(float x)
    {
        if(x > 0 && !isRight)
        {
            Rotate();
        }
        else if(x < 0 && isRight)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        directionRight *= -1f;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGround = false;
    }

}
