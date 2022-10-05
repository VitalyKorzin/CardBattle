using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class SoundOfWeaponUsing : Sound
{
    private Weapon _weapon;

    private void OnEnable() 
        => _weapon.Used += OnWeaponUsed;

    private void OnDisable() 
        => _weapon.Used -= OnWeaponUsed;

    protected override void Awake()
    {
        base.Awake();
        _weapon = GetComponent<Weapon>();
    }

    private void OnWeaponUsed() => Play();
}