using UnityEngine;

public class ArmorHitSound : Sound
{
    [SerializeField] private Protection _protection;

    private void OnEnable() 
        => _protection.TookDamage += OnProtectionTookDamage;

    private void OnDisable() 
        => _protection.TookDamage -= OnProtectionTookDamage;

    private void OnProtectionTookDamage() => Play();
}