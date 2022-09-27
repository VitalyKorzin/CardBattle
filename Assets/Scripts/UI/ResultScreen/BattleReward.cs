using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BattleReward : ResultDisplayElement
{
    [Min(0)]
    [SerializeField] private int _value;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SdkYandex _sdkYandex;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Pointer _pointer;

    private int _valueMultiplied;

    public int Value => _value;

    private void OnEnable()
    {
        _sdkYandex.Rewarded += OnRewarded;

        if (_pointer != null)
            _pointer.RewardMultiplied += OnRewardMultiplied;
    }

    private void OnDisable()
    {
        _sdkYandex.Rewarded -= OnRewarded;

        if (_pointer != null)
            _pointer.RewardMultiplied -= OnRewardMultiplied;
    }

    private void Start()
        => DisplayValue();

    public override void Appear(float endValue, float duration)
    {
        if (endValue < 0)
            throw new ArgumentOutOfRangeException(nameof(endValue));

        if (duration < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        _text.DOFade(endValue, duration);
    }

    public void ReplenishWallet() => _wallet.Replenish(_value);

    private void OnRewarded()
    {
        _value = _valueMultiplied;
        DisplayValue();
    }

    private void OnRewardMultiplied(int multiplier) 
        => _valueMultiplied = _value * multiplier;

    private void DisplayValue() => _text.text = _value.ToString();
}