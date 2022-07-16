using System;
using System.Collections;
using UnityEngine;

public class CardsDispenser : MonoBehaviour
{
    [SerializeField] private Card[] _cards;
    [SerializeField] private CardsDeck _cardsDeck;
    [Min(0)]
    [SerializeField] private float _secondsBetweenDispensing;

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

    public void Start() => StartCoroutine(Dispense());

    private IEnumerator Dispense()
    {
        var _delay = new WaitForSeconds(_secondsBetweenDispensing);

        for (var i = 0; i < _cardsDeck.MaximumCount; i++)
        {
            var card = Instantiate(_cards[i], transform.position, Quaternion.identity, _cardsDeck.transform);
            _cardsDeck.Add(card);
            yield return _delay;
        }
    }

    private void Validate()
    {
        if (_cardsDeck == null)
            throw new InvalidOperationException();

        if (_cards == null)
            throw new InvalidOperationException();

        if (_cards.Length != _cardsDeck.MaximumCount)
            throw new InvalidOperationException();
    }
}