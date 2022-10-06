using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(TMP_Text))]
public class BattleResultDisplay : ResultDisplayElement
{
    [SerializeField] private Image _background;

    public override void Appear(float endValue, float duration)
    {
        if (endValue < 0)
            throw new ArgumentOutOfRangeException(nameof(endValue));

        if (duration < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        _background.DOFade(endValue, duration);
        GetComponent<TMP_Text>().DOFade(endValue, duration);
    }
}