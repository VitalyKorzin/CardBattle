using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectricballCard : Card
{
    [SerializeField] private Vector3 _spawnPositionOffset;
    [SerializeField] private Electricball _template;

    public override void Use(IReadOnlyList<Stickman> stickmen, Vector3 actionPosition) 
        => Instantiate(_template, actionPosition + _spawnPositionOffset, Quaternion.identity);
}