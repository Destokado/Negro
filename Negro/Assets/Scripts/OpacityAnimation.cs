using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpacityAnimation : MonoBehaviour
{
    [SerializeField] protected AnimationCurve animationCurve;
    protected float objectiveVal;
    protected float originalVal;
    protected float animationDuration;
    protected float currentAnimationElapsedTime;
    
    
    public abstract void SetOpacityTo(float value, float time);

    protected static float GetValueTroughAnimCurve(float origin, float objective, float timeElapsed, float animationDuration, AnimationCurve animationCurve)
    {
        float progress = animationDuration > 0 ? animationCurve.Evaluate(timeElapsed / animationDuration) : 1;
        return Mathf.Lerp(origin, objective, progress);
    }
}
