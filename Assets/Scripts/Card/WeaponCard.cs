using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : Card
{
    [SerializeField] private Weapon _template;

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
        {
            if (stickman.gameObject.TryGetComponent(out Armament armament))
                armament.Give(_template);
            else
                throw new InvalidOperationException();
        }
    }
}