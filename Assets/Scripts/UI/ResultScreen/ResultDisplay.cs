using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _appearanceDuration;
    [Min(0)]
    [SerializeField] private float _peakSize;
    [Min(0)]
    [SerializeField] private float _startSize;
    [Min(0)]
    [SerializeField] private float _targetSize;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private ResultDisplayElement[] _elements;

    private readonly float _appearanceEndValue = 1f;

    private float _scalingDuration;

    private void Start()
    {
        transform.localScale = new Vector3(_startSize, _startSize, _startSize);
        _scalingDuration = _appearanceDuration / 2f;
    }

    public void Display()
    {
        Appear();
        AppearElements();
    }

    private void Appear()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(_peakSize, _scalingDuration));
        sequence.Append(transform.DOScale(_targetSize, _scalingDuration));
    }

    private void AppearElements()
    {
        _label.DOFade(_appearanceEndValue, _appearanceDuration);

        foreach (var element in _elements)
            element.Appear(_appearanceEndValue, _appearanceDuration);
    }
}