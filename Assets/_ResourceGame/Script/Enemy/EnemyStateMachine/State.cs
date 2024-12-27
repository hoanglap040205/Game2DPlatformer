using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public virtual void OnEnter(){ }
    public virtual void OnUpdate(){ }
    public virtual void OnHurt(){}
    public virtual void OnExit(){ }
    
}

class PatrolState : State
{
    public override void OnEnter(){ }

    public override void OnUpdate(){}
    public override void OnHurt()
    {
    }

    public override void OnExit(){}
    
}

class ChaseStae: State
{
    public override void OnEnter(){ }
    public override void OnUpdate(){}
    public override void OnHurt(){}
    public override void OnExit(){}
}

class AttackState : State
{
    public override void OnEnter()
    {
    }
    public override void OnUpdate(){}
    public override void OnHurt(){}
    public override void OnExit(){}
}

class Death : State
{
    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
    }
    
    public override void OnHurt(){}
    public override void OnExit(){}
}


