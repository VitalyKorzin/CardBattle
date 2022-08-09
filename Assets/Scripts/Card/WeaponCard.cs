using System;
using System.Collections.Generic;
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

    public override void Use<T>(List<T> stickmen, Vector3 actionPosition)
    {
        if (stickmen == null)
            throw new ArgumentNullException(nameof(stickmen));

        foreach (var stickman in stickmen)
            stickman.GiveWeapon(_template, _additionalHealth, _additionalDamage);
    }
}