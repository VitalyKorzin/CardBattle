using System;
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

    public override void Use(CardActionArea actionArea)
    {
        if (actionArea == null)
            throw new ArgumentNullException(nameof(actionArea));

        foreach (var stickman in actionArea.Stickmen)
            stickman.GiveArmor(_shield, _helmet, _additionalHealth, _additionalDamage);
    }

    private void Validate()
    {
        if (_shield == null)
            throw new InvalidOperationException();

        if (_helmet == null)
            throw new InvalidOperationException();
    }
}