using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageOpacityAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    private Image image;
    private float objectiveVal;
    private float originalVal;
    private float animationDuration;
    private float currentAnimationElapsedTime;
    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetOpacityTo(float value, float time)
    {
        originalVal = image.color.a;
        objectiveVal = value;
        animationDuration = time;
        currentAnimationElapsedTime = 0f;
    }

    private void Update()
    {
        image.raycastTarget = Math.Abs(image.color.a) > 0.05f;

        if (Math.Abs(objectiveVal - image.color.a) < 0.001f)
            return;
            
        
        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, GetValueTroughAnimCurve(originalVal, objectiveVal, currentAnimationElapsedTime, animationDuration, animationCurve));
        
        currentAnimationElapsedTime += Time.deltaTime;
    }

    public static float GetValueTroughAnimCurve(float origin, float objective, float timeElapsed, float animationDuration, AnimationCurve animationCurve)
    {
        float progress = animationDuration > 0 ? animationCurve.Evaluate(timeElapsed / animationDuration) : 1;
        return Mathf.Lerp(origin, objective, progress);
    }
}
