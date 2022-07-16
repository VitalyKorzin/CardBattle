using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class AdditionalRewardDisplay : ResultDisplayElement
{
    [SerializeField] private BattleReward _battleReward;
    [SerializeField] private Pointer _pointer;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch (Exception exception)
        {
            enabled = false;
            throw exception;
        }

        _pointer.RewardMultiplied += OnRewardMultiplied;
    }

    private void OnDisable() 
        => _pointer.RewardMultiplied -= OnRewardMultiplied;

    public override void Appear(float endValue, float duration) 
        => _text.DOFade(endValue, duration);

    private void OnRewardMultiplied(int multiplier)
        => _text.text = "+" + (_battleReward.Value * multiplier).ToString();

    private void Validate()
    {
        if (_battleReward == null)
            throw new InvalidOperationException();

        if (_pointer == null)
            throw new InvalidOperationException();

        if (_text == null)
            throw new InvalidOperationException();
    }
}