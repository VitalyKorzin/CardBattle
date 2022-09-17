using System;
using UnityEngine;
using UnityEngine.Events;

public class Protection : MonoBehaviour
{
    [SerializeField] private Helmet _helmet;
    [SerializeField] private Shield _shield;
    [SerializeField] private Transform _shieldPosition;
    [SerializeField] private Transform _helmetPosition;
    [SerializeField] private ParticleSystem _buff;

    private Helmet _currentHelmet;
    private Shield _currentShield;

    public int Value { get; private set; }

    public event UnityAction<int> Changed;
    public event UnityAction ArmorGived;

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
    }

    public void Give(Helmet helmet) 
        => Give(helmet, _helmetPosition, ref _currentHelmet);

    public void Give(Shield shield) 
        => Give(shield, _shieldPosition, ref _currentShield);

    private void Give<T>(T newArmor, Transform position, ref T currentArmor) where T: Armor
    {
        if (newArmor == null)
            throw new ArgumentNullException(nameof(newArmor));

        Dress(newArmor, position, ref currentArmor);
        _buff.Play();
    }

    private void Dress<T>(T newArmor, Transform position, ref T currentArmor) where T : Armor
    {
        if (currentArmor != null)
            Destroy(currentArmor.gameObject);

        if (Value == 0)
            ArmorGived?.Invoke();

        currentArmor = Instantiate(newArmor, position);
        Strengthen(currentArmor.Value);
    }

    private void Strengthen(int value)
    {
        Value += value;
        Changed?.Invoke(Value);
    }

    private void TryDressArmors()
    {
        if (_helmet != null)
            Dress(_helmet, _helmetPosition, ref _currentHelmet);

        if (_shield != null)
            Dress(_shield, _shieldPosition, ref _currentShield);
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