using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawImageOpacityAnimation : OpacityAnimation
{
    private RawImage image;

    public override void SetOpacityTo(float value, float time)
    {
        if (image == null)
        {
            image = GetComponent<RawImage>();
            objectiveVal = image.color.a;
        }
        
        originalVal = image.color.a;
        objectiveVal = value;
        animationDuration = time;
        currentAnimationElapsedTime = 0f;
    }

    private void Update()
    {
        if (image == null)
            return;

        image.raycastTarget = Mathf.Abs(image.color.a) > 0.05f;

        if (Mathf.Abs(objectiveVal - image.color.a) < 0.001f)
            return;

        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, GetValueTroughAnimCurve(originalVal, objectiveVal, currentAnimationElapsedTime, animationDuration, animationCurve));
        
        currentAnimationElapsedTime += Time.deltaTime;
    }


}
