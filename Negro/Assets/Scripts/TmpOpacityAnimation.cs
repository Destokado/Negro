using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TmpOpacityAnimation : OpacityAnimation
{
    private TextMeshProUGUI tmProGui;

    public override void SetOpacityTo(float value, float time)
    {
        if (tmProGui == null)
        {
            tmProGui = GetComponent<TextMeshProUGUI>();
            objectiveVal = tmProGui.color.a;
        }
        
        originalVal = tmProGui.color.a;
        objectiveVal = value;
        animationDuration = time;
        currentAnimationElapsedTime = 0f;
    }

    private void Update()
    {
        if (tmProGui == null)
            return;
        
        tmProGui.raycastTarget = Mathf.Abs(tmProGui.color.a) > 0.05f;

        if (Mathf.Abs(objectiveVal - tmProGui.color.a) < 0.001f)
            return;

        Color currentColor = tmProGui.color;
        tmProGui.color = new Color(currentColor.r, currentColor.g, currentColor.b, GetValueTroughAnimCurve(originalVal, objectiveVal, currentAnimationElapsedTime, animationDuration, animationCurve));
        
        currentAnimationElapsedTime += Time.deltaTime;
    }
}
