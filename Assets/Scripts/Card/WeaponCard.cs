using System;
using UnityEngine;
public class WeaponCard : Card
{
    [Min(0)]
    [SerializeField] private int _additionalHealth;
    [Min(0)]
    [SerializeField] private int _additionalDamage;
    [SerializeField] private Sword _template;

    private void OnEnable()
    {
        if (_template == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    public override void Use(CardActionArea actionArea)
    {
        if (actionArea == null)
            throw new ArgumentNullException(nameof(actionArea));

        foreach (var stickman in actionArea.Stickmen)
            stickman.GiveWeapon(_template, _additionalHealth, _additionalDamage);
    }
}