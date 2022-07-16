using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BattleReward : ResultDisplayElement
{
    [Min(0)]
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
        => _text.text = _value.ToString();

    public override void Appear(float endValue, float duration) 
        => _text.DOFade(endValue, duration);
}