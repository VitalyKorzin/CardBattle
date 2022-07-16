using UnityEngine;

public class WeaponCard : Card
{
    [Min(0)]
    [SerializeField] private int _additionalHealth;
    [Min(0)]
    [SerializeField] private int _additionalDamage;

    protected override void Action(Stickman stickman)
        => stickman.GiveWeapon(_additionalHealth, _additionalDamage);
}