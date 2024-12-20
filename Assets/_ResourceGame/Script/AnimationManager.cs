using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private AnimationState currentState = AnimationState.IDLE;
    public readonly string xVelocity = "xVelocity";
    public readonly string yVelocity = "yVelocity";

    void Start()
    {
        animator = GetComponent<Animator>();

        enterState(currentState);
    }

    public void animFloat(string nameAnim, float value)
    {
        animator.SetFloat(nameAnim, value);
    }

    private void enterState(AnimationState _state)
    {
        animator.SetBool(_state.ToString(), true);
    }

    private void exitState(AnimationState _state)
    {
        animator.SetBool(_state.ToString(), false);
    }

    public void changeState(AnimationState _state)
    {
        if (_state == currentState)
        {
            return;
        }
        exitState(currentState); // Thoát state hiện tại
        currentState = _state; // cập nhập state mới
        enterState(currentState); // Bắt đầu state
    }
}

public enum AnimationState
{
    IDLE,
    RUN,
    JUMP,
    CROUNCH
}

