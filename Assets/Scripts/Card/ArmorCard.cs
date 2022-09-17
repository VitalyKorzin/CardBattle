using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCard : Card
{
    [Min(0)]
    [SerializeField] private int _additionalHealth;
    [Min(0)]
    [SerializeField] private int _additionalDamage;
    [SerializeField] private Shield _shield;
    [SerializeField] private Helmet _helmet;

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

    public override void Use<T>(List<T> stickmen, Vector3 actionPosition)
    {
        if (stickmen == null)
            throw new ArgumentNullException(nameof(stickmen));

        foreach (var stickman in stickmen)
        {
            if (stickman.gameObject.TryGetComponent(out Protection protection))
            {
                protection.Give(_helmet);
                protection.Give(_shield);
            }
        }
    }

    private void Validate()
    {
        if (_shield == null)
            throw new InvalidOperationException();

        if (_helmet == null)
            throw new InvalidOperationException();
    }
}