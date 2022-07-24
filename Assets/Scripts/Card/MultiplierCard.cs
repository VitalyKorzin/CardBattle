using System;
using UnityEngine;
using UnityEngine.Events;

public class MultiplierCard : Card
{
    [Min(1)]
    [SerializeField] private int _value;

    public int SitckmenCount => _value - 1;

    public event UnityAction<MultiplierCard, CardActionArea> Used;

    public override void Use(CardActionArea actionArea)
    {
        if (actionArea == null)
            throw new ArgumentNullException(nameof(actionArea));

        Used?.Invoke(this, actionArea);
    }
}