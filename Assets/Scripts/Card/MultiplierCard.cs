using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MultiplierCard : Card
{
    [Min(1)]
    [SerializeField] private int _value;
    [SerializeField] private TMP_Text _valueText;

    public int SitckmenCount => _value - 1;

    public event UnityAction<MultiplierCard, Stickman> Used;

    private void OnEnable()
    {
        if (_valueText == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    private void Start()
        => _valueText.text += _value.ToString();

    protected override void Action(Stickman stickman)
        => Used?.Invoke(this, stickman);
}