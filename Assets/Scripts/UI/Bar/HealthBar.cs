using System;
using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Health _health;

    protected override int GetCurrentValue() => _health.Value;

    protected override void SubscribeToEvents() 
        => _health.Changed += OnValueChanged;

    protected override void UnsubscribeFromEvents() 
        => _health.Changed -= OnValueChanged;

    protected override void Validate()
    {
        base.Validate();

        if (_health == null)
            throw new InvalidOperationException();
    }
}