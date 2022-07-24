using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class NextButton : ResultDisplayElement
{
    public override void Appear(float endValue, float duration)
    {
        if (endValue < 0)
            throw new ArgumentOutOfRangeException(nameof(endValue));

        if (duration < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        GetComponent<TMP_Text>().DOFade(endValue, duration);
    }
}