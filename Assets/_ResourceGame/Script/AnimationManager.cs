using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public AnimationState currentState = AnimationState.IDLE;
    public readonly string xVelocity = "xVelocity";
    public readonly string yVelocity = "yVelocity";
    public readonly string gunXvelocity = "gunVelocity";
    public readonly string gunYvelocity = "gunYvelocity";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public AnimationManager changeAnim(AnimationState _state)
    {
        currentState = _state;
        animator.SetTrigger(_state.ToString());
        return this;
    }

    public AnimationManager changeBlend(string name , float value)
    {
        animator.SetFloat(name, value);
        return this;
    }

    public AnimationManager changeBool(string name , bool value)
    {
        animator.SetBool(name, value);
        return this;
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
    CROUNCH_GUN
}

