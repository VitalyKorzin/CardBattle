using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Protection : MonoBehaviour
{
    [SerializeField] private List<Armor> _armors;
    [SerializeField] private Transform _shieldPosition;
    [SerializeField] private Transform _helmetPosition;
    [SerializeField] private ParticleSystem _buff;

    private Helmet _helmet;
    private Shield _shield;

    public int Value { get; private set; }

    public event UnityAction<int> Changed;
    public event UnityAction ArmorGived;
    public event UnityAction TookDamage;

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
    }

    private void Start() => TryDressArmors();

    public void Apply(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        Value = Math.Clamp(Value - damage, 0, Value);
        Changed?.Invoke(Value);
        TookDamage?.Invoke();
    }

    public void Give(Armor armor, bool buffPlays = true)
    {
        if (armor is Helmet helmet)
            Give(helmet, buffPlays);
        else if (armor is Shield shield)
            Give(shield, buffPlays);
    }

    private void Give(Helmet helmet, bool buffPlays = true) 
        => Give(helmet, _helmetPosition, ref _helmet, buffPlays);

    private void Give(Shield shield, bool buffPlays = true) 
        => Give(shield, _shieldPosition, ref _shield, buffPlays);

    private void Give<T>(T newArmor, Transform position, ref T currentArmor, bool buffPlays = true) where T: Armor
    {
        if (newArmor == null)
            throw new ArgumentNullException(nameof(newArmor));

        Dress(newArmor, position, ref currentArmor);

        if (buffPlays)
            _buff.Play();
    }

    private void Dress<T>(T newArmor, Transform position, ref T currentArmor) where T : Armor
    {
        if (Value == 0)
            ArmorGived?.Invoke();

        currentArmor = Instantiate(newArmor, position);

        if (_armors.Contains(newArmor) == false)
            _armors.Add(currentArmor);

        Strengthen(currentArmor.Value);
    }

    private void Strengthen(int value)
    {
        Value += value;
        Changed?.Invoke(Value);
    }

    private void TryDressArmors()
    {
        foreach (Armor armor in _armors)
            Give(armor, false);
    }

    private void Validate()
    {
        if (_shieldPosition == null)
            throw new InvalidOperationException();

        if (_helmetPosition == null)
            throw new InvalidOperationException();

        if (_buff == null)
            throw new InvalidOperationException();
    }
}