using UnityEngine;

public class AnimationManager : AnimationBase
{
    public Animator animator;
    public AnimationState currentState = AnimationState.IDLE;
    public readonly string xVelocity = "xVelocity";
    public readonly string yVelocity = "yVelocity";
    public readonly string gunXvelocity = "gunVelocity";
    public readonly string gunYvelocity = "gunYvelocity";

    [Header("Attack Infor")]
    [SerializeField] private float radiusAttack;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask Mask;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override AnimationBase changeAnim(AnimationState _state)
    {
        currentState = _state;
        animator.SetTrigger(_state.ToString());
        return this;
    }

    public override AnimationBase changeBlend(string name , float value)
    {
        animator.SetFloat(name, value);
        return this;
    }

    public override AnimationBase changeBool(string name , bool value)
    {
        animator.SetBool(name, value);
        return this;
    }

    public void AttackEventTrigger()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPos.position , radiusAttack , Mask);
        foreach(var i in hit)
        {
            if(i != null)
            {
                Debug.Log(i.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, radiusAttack);
    }
}

public enum AnimationState
{
    IDLE,
    RUN,
    JUMP,
    CROUNCH,
    ATTACK,
    IDLE_GUN,
    RUN_GUN,
    JUMP_GUN,
    CROUNCH_GUN,
    HURT,
    DIE
}

