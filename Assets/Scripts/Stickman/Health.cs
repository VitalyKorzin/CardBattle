using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _value;
    [SerializeField] private ParticleSystem _healing;

    public int Value => _value;

    public event UnityAction<int> Changed;
    public event UnityAction<Health, int> Healed;

    private void OnEnable()
    {
        if (_healing == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    public void Apply(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _value = Math.Clamp(_value - damage, 0, _value);
        Changed?.Invoke(_value);
    }

    public void Heal(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _value += value;
        Changed?.Invoke(_value);
        Healed?.Invoke(this, value);
        _healing.Play();
    }
}