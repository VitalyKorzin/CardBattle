using UnityEngine;

public class ArmorCard : Card
{
    [Min(0)]
    [SerializeField] private int _additionalHealth;
    [Min(0)]
    [SerializeField] private int _additionalDamage;

    protected override void Action(Stickman stickman)
        => stickman.GiveArmor(_additionalHealth, _additionalDamage);
}