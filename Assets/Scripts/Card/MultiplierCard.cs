using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiplierCard : Card
{
    [Min(1)]
    [SerializeField] private int _value;

    public int SitckmenCount => _value - 1;

    public event UnityAction<MultiplierCard, List<Stickman>> Used;

    public override void Use<T>(List<T> stickmen, Vector3 actionPosition)
    {
        if (stickmen == null)
            throw new ArgumentNullException(nameof(stickmen));

        List<Stickman> selectedStickmen = new List<Stickman>();

        foreach (Stickman stickman in stickmen)
            selectedStickmen.Add(stickman);

        Used?.Invoke(this, selectedStickmen);
    }
}