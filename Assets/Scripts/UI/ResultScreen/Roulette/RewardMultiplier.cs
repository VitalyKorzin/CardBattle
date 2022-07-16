using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class RewardMultiplier : ResultDisplayElement
{
    [Min(1)]
    [SerializeField] private int _value;
    [SerializeField] private TMP_Text _text;

    public int Value => _value;

    private void OnEnable()
    {
        if (_text == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    private void Start()
        => _text.text = "x" + _value.ToString();

    public override void Appear(float endValue, float duration)
    {
        _text.DOFade(endValue, duration);
        GetComponent<Image>().DOFade(endValue, duration);
    }
}