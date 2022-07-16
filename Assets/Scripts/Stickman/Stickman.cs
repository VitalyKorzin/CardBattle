using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Stickman : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _health;
    [Min(1)]
    [SerializeField] private int _damage;

    public int Health => _health;
    public int Damage => _damage;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<Stickman> Died;
    public event UnityAction<StickmenSquad> FightStarted;
    public event UnityAction<PlaceInSquad> AddedToSquad;

    public void AddToSquad(PlaceInSquad place)
    {
        if (place == null)
            throw new ArgumentNullException(nameof(place));

        AddedToSquad?.Invoke(place);
    }

    public void StartFight(StickmenSquad enemies)
    {
        if (enemies == null)
            throw new ArgumentNullException(nameof(enemies));

        FightStarted?.Invoke(enemies);
    }

    public void GiveArmor(int additionalHealth, int additionalDamage)
        => Strengthen(additionalHealth, additionalDamage);

    public void GiveWeapon(int additionalHealth, int additionalDamage)
        => Strengthen(additionalHealth, additionalDamage);

    public void Apply(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _health -= damage;
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Die();
    }

    public void Heal(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _health += value;
        HealthChanged?.Invoke(_health);
    }

    private void Strengthen(int additionalHealth, int additionalDamage)
    {
        if (additionalHealth < 0)
            throw new ArgumentOutOfRangeException(nameof(additionalHealth));

        if (additionalDamage < 0)
            throw new ArgumentOutOfRangeException(nameof(additionalDamage));

        Heal(additionalHealth);
        _damage += additionalDamage;
    }

    private void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}