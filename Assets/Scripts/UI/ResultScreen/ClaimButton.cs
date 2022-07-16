using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ClaimButton : ResultDisplayElement
{
    [Min(0)]
    [SerializeField] private float _pulsationSpeed;
    [Min(0)]
    [SerializeField] private float _pulsationEndValue;
    [SerializeField] private TMP_Text _text;

    private readonly int _loops = -1;

    private void OnEnable()
    {
        if (_text == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    public override void Appear(float endValue, float duration)
    {
        _text.DOFade(endValue, duration);
        GetComponent<Image>().DOFade(endValue, duration);
        Pulsate();
    }

    private void Pulsate()
        => transform.DOScale(_pulsationEndValue, _pulsationSpeed).SetLoops(_loops, LoopType.Yoyo);

}