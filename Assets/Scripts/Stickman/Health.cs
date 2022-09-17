using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _startValue;
    [SerializeField] private ParticleSystem _healing;

    public int Value { get; private set; }

    public event UnityAction<int> Changed;

    private void OnEnable()
    {
        if (_healing == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    private void Awake() => Value = _startValue;

    public void Apply(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        Value = Math.Clamp(Value - damage, 0, Value);
        Changed?.Invoke(Value);
    }

    public void Heal(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        Value += value;
        Changed?.Invoke(Value);
        _healing.Play();
    }
}