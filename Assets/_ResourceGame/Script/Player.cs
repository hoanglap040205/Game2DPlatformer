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

    [Header("Ground check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask WhatIsMask;


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

        anim.changeBool("Ground", IsGround());
        anim.changeBool("AttackMelee", canMeleeAttacking);
        anim.changeBool(AnimationState.ATTACK.ToString(), isGunAttacking);
        anim.changeBool(AnimationState.CROUNCH.ToString(), isCrounching);
        anim.changeBlend(isGunAttacking ? anim.gunYvelocity : anim.yVelocity, rb.velocity.y);
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
        if (!IsGround()) return;

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
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            //isGround = false;
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
        if (Input.GetKey(KeyCode.O))
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

    private bool IsGround() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, WhatIsMask);


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheck.position , new Vector3(groundCheck.position.x , groundCheck.position.y - groundDistance , groundCheck.position.z));
    }
}
