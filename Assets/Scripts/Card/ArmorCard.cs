using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCard : Card
{
    [SerializeField] private Armor _template;

    public override void Use(IReadOnlyList<Stickman> stickmen, Vector3 actionPosition)
    {
        if (stickmen == null)
            throw new ArgumentNullException(nameof(stickmen));

        foreach (var stickman in stickmen)
        {
            if (stickman.gameObject.TryGetComponent(out Protection protection))
                protection.Give(_template);
            else
                throw new InvalidOperationException();
        }
    }
}