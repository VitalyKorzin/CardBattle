using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectricballCard : Card
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Electricball _template;

    private void OnEnable()
    {
        if (_template == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    public override void Use<T>(List<T> stickmen, Vector3 actionPosition) 
        => Instantiate(_template, actionPosition + _offset, Quaternion.identity);
}