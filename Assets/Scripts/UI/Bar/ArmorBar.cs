using System;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : Bar
{
    [SerializeField] private Protection _protection;
    [SerializeField] private Image _icon;

    protected override int GetCurrentValue() => _protection.Value;

    protected override void SubscribeToEvents()
    {
        _protection.Changed += OnValueChanged;
        _protection.ArmorGived += OnArmorGived;
    }

    protected override void UnsubscribeFromEvents()
    {
        _protection.Changed -= OnValueChanged;
        _protection.ArmorGived -= OnArmorGived;
    }

    protected override void Validate()
    {
        base.Validate();

        if (_protection == null)
            throw new InvalidOperationException();

        if (_icon == null)
            throw new InvalidOperationException();
    }

    protected override void TryDisableIcon()
    {
        if (GetCurrentValue() == 0)
            _icon.gameObject.SetActive(false);
    }

    private void OnArmorGived() => _icon.gameObject.SetActive(true);
}