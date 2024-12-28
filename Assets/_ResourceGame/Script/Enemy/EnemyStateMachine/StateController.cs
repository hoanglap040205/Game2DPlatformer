using UnityEngine;

public class StateController : MonoBehaviour
{
   public PatrolState patrolState = new PatrolState();
   public ChaseState chaseState = new ChaseState();
   public AttackState attackState = new AttackState();
   public Death death = new Death();
    public Animator anim;
    State currentState;
    private void Awake()
    {
     anim = GetComponent<Animator>(); 
     Debug.Log("StateController::Awake");
     ChangeState(patrolState);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            Debug.Log("Da chay vao day");
            currentState.OnExit();  
        }
        currentState = newState;
        currentState.OnEnter(this);
    }
}