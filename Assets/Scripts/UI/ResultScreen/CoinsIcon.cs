using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CoinsIcon : ResultDisplayElement
{
    [Min(0)]
    [SerializeField] private float _pulsationSpeed;
    [Min(0)]
    [SerializeField] private float _pulsationEndValue;

    public override void Appear(float endValue, float duration)
    {
        if (endValue < 0)
            throw new ArgumentOutOfRangeException(nameof(endValue));

        if (duration < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        GetComponent<Image>().DOFade(endValue, duration);
        Pulsate();
    }

    private void Pulsate()
        => transform.DOScale(_pulsationEndValue, _pulsationSpeed).SetLoops(-1, LoopType.Yoyo);
}