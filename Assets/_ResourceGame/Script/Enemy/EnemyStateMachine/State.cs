using UnityEngine;

public abstract class State
{
    public StateController sc;
    public virtual void OnEnter(StateController stateController)
    {
       this.sc = stateController;
    }
    public virtual void OnUpdate(){ }
    public virtual void OnHurt(){}
    public virtual void OnExit(){ }
    
}

 public class PatrolState : State
{
    public override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);
        Debug.Log("Patrol State");

    }

    public override void OnUpdate()
    {
        Debug.Log("dang di bo");
        sc.GetComponent<StateController>().anim.Play("Walk");

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Doi trang thai");
            sc.ChangeState(sc.chaseState);

        }
    }
    public override void OnHurt()
    {
        
    }

    public override void OnExit()
    {
        Debug.Log("Thoat khoi trang thai");

    }
    
}

public class ChaseState: State
{
    public override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);
        Debug.Log("ChaseStae State");
    }

    public override void OnUpdate()
    {
        sc.GetComponent<StateController>().anim.Play("Run");
        Debug.Log("Chasing State");
    }
    public override void OnHurt(){}
    public override void OnExit(){}
}

public class AttackState : State
{
    public override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);
    }
    public override void OnUpdate(){}
    public override void OnHurt(){}
    public override void OnExit(){}
}

public class Death : State
{
    public override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);
    }

    public override void OnUpdate()
    {
    }
    
    public override void OnHurt(){}
    public override void OnExit(){}
}