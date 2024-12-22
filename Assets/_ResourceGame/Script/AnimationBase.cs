using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationBase : MonoBehaviour
{
    protected AnimationState currenState;

    public abstract AnimationBase changeAnim(AnimationState state);
    public abstract AnimationBase changeBlend(string name , float value);
    public abstract AnimationBase changeBool(string name , bool value);

}
