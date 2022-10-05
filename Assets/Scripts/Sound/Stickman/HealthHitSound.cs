using UnityEngine;

public class HealthHitSound : Sound
{
    [SerializeField] private Health _health;

    private void OnEnable() 
        => _health.TookDamage += OnHealthTookDamage;

    private void OnDisable() 
        => _health.TookDamage -= OnHealthTookDamage;

    private void OnHealthTookDamage() => Play();
}