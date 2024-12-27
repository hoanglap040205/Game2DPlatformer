using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAttack playerAttack;
    [SerializeField] private AnimationManager anim;

    private float inputValue;
    private string blendValue;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGround;
    [SerializeField] private bool isCrounching;

    [Header("Attack Melee")]
    [SerializeField] private bool canMeleeAttacking;
    [SerializeField] private float attackCoolDown;
    private float lastTimeAttack;

    [Header("Attack Gun")]
    [SerializeField] private bool isGunAttacking;
    [SerializeField] private float fireCoolDown;
    [SerializeField] private float fireLastTime;



    [Header("Rotate")]
    private float directionRight;
    private bool isRight;

    public float InputValue { get => inputValue; set => inputValue = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        isRight = true;
        directionRight = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        InputValue = Input.GetAxis("Horizontal");

        lastTimeAttack -= Time.deltaTime;
        if(lastTimeAttack < 0f && canMeleeAttacking)
        {
            canMeleeAttacking = false;
        }
        Movement();
        AttackGun();
        AttackMelee();

        RotateControler(InputValue);
        Jump();

        Crounch();

        anim.changeBool("Ground", isGround);
        anim.changeBool("AttackMelee", canMeleeAttacking);
        anim.changeBool(AnimationState.ATTACK.ToString(), isGunAttacking);
        anim.changeBool(AnimationState.CROUNCH.ToString(), isCrounching);
        anim.changeBlend(isGunAttacking ? anim.gunYvelocity : anim.yVelocity, Mathf.Clamp(rb.velocity.y, -jumpForce, jumpForce));
        anim.changeBlend(isGunAttacking ? anim.gunXvelocity : anim.xVelocity, Mathf.Abs(inputValue));
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(InputValue * speed, rb.velocity.y);

        if (canMeleeAttacking)
        {
            rb.velocity = new Vector2(directionRight * 0.7f, rb.velocity.y);
            return;
        }
    }

    private void Movement()
    {
        if (InputValue != 0)
        {
            anim.changeAnim(isGunAttacking ? AnimationState.RUN_GUN : AnimationState.RUN);
            InputValue /= 2f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                InputValue *= 2;
            }

        }
        else
        {
            anim.changeAnim(isGunAttacking ? AnimationState.IDLE_GUN : AnimationState.IDLE);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;
            anim.changeAnim(isGunAttacking ? AnimationState.JUMP_GUN : AnimationState.JUMP);
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

    private void AttackGun()
    {
        if (Input.GetKey(KeyCode.F))
        {
            isGunAttacking = true;
            if(Time.time > fireLastTime + fireCoolDown && isGunAttacking)
            {
                playerAttack.gunFire();
                fireLastTime = Time.time;
            }
        }
        else
        {
            isGunAttacking = false;
        }
    }

    private void AttackMelee()
    {
        if(Input.GetKeyDown(KeyCode.J) && !canMeleeAttacking)
        {
            canMeleeAttacking = true;
            lastTimeAttack = attackCoolDown;
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

    public void playerHurt() => anim.changeAnim(AnimationState.HURT).changeAnim(AnimationState.IDLE);

    public void playerDie() => anim.changeAnim(AnimationState.DIE);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGround = false;
    }

}
