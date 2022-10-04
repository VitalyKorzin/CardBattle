using System;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteFall : MonoBehaviour, IPlayerAbility
{
    [SerializeField] private Vector3 _spawnPositionOffset;
    [SerializeField] private Meteorite _template;

    private void OnEnable()
    {
        if (_template == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    public void Use(IReadOnlyList<Stickman> stickmen, Vector3 actionPosition)
        => Instantiate(_template, actionPosition + _spawnPositionOffset, Quaternion.identity);
}