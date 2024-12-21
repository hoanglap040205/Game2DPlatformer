using UnityEditor.U2D.Sprites;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private AnimationManager anim;

    private float inputValue;
    private string blendValue;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGround;
    [SerializeField] private float timeState;
    [SerializeField] private bool isCrounching;
    [SerializeField] private bool isAttacking;

    [Header("Rotate")]
    private float directionRight;
    private bool isRight;

    public float InputValue { get => inputValue; set => inputValue = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isRight = true;
        directionRight = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        InputValue = Input.GetAxis("Horizontal");


        Movement();
        Attack();
        RotateControler(InputValue);
        Jump();

        Crounch();

        anim.changeBool("Ground", isGround);
        anim.changeBool(AnimationState.ATTACK.ToString(), isAttacking);
        anim.changeBool(AnimationState.CROUNCH.ToString(), isCrounching);

        anim.changeBlend(isAttacking ? anim.gunYvelocity : anim.yVelocity, Mathf.Clamp(rb.velocity.y, -jumpForce, jumpForce));
        anim.changeBlend(isAttacking ? anim.gunXvelocity : anim.xVelocity , Mathf.Abs(inputValue));
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(InputValue * speed, rb.velocity.y);
    }

    private void Movement()
    {
        if (InputValue != 0)
        {
            anim.changeAnim(isAttacking ? AnimationState.RUN_GUN : AnimationState.RUN);
            InputValue /= 2f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                InputValue *= 2;
            }
            
        }
        else
        {
            anim.changeAnim(isAttacking ? AnimationState.IDLE_GUN : AnimationState.IDLE);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;
            anim.changeAnim(isAttacking ? AnimationState.JUMP_GUN : AnimationState.JUMP);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Crounch()
    {
        if (Input.GetKey(KeyCode.S))
        {
            isCrounching = true;
            inputValue = 0;
        }
        else
        {
            if (isCrounching)
            {
                isCrounching = false;
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.O))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    private void RotateControler(float x)
    {
        if (x > 0 && !isRight)
        {
            Rotate();
        }
        else if (x < 0 && isRight)
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

    public float xVelocity => rb.velocity.x;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGround = false;
    }

}
