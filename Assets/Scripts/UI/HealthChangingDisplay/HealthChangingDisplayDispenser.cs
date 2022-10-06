using System;
using UnityEngine;

public class HealthChangingDisplayDispenser : MonoBehaviour
{
    [SerializeField] private EnemiesSquad _enemiesSquad;
    [SerializeField] private HeroesSquad _heroesSquad;
    [SerializeField] private HealthChangingDisplay _displayTempalte;

    private void OnEnable()
    {
        _enemiesSquad.StickmanAdded += OnStickmanAdded;
        _heroesSquad.StickmanAdded += OnStickmanAdded;
    }

    private void OnDisable()
    {
        _enemiesSquad.StickmanAdded -= OnStickmanAdded;
        _heroesSquad.StickmanAdded -= OnStickmanAdded;
    }

    private void OnStickmanAdded(Stickman stickman)
    {
        stickman.Died += OnStickmanDied;
        stickman.DamageReceived += OnDamageReceived;
        Health health = GetHealth(stickman);
        health.Healed += OnStickmanHealed;
    }

    private void OnStickmanDied(Stickman stickman)
    {
        stickman.Died -= OnStickmanDied;
        stickman.DamageReceived -= OnDamageReceived;
        Health health = GetHealth(stickman);
        health.Healed -= OnStickmanHealed;
    }

    private Health GetHealth(Stickman stickman)
    {
        if (stickman.TryGetComponent(out Health health))
            return health;
        else
            throw new InvalidOperationException();
    }

    private void OnDamageReceived(Stickman stickman, int damage) 
        => CreateDisplay(stickman.transform.position).DisplayDamageReceiving(damage);

    private void OnStickmanHealed(Health health, int value) 
        => CreateDisplay(health.transform.position).DisplayHealthReceiving(value);

    private HealthChangingDisplay CreateDisplay(Vector3 position)
        => Instantiate(_displayTempalte, position, Quaternion.identity);
}