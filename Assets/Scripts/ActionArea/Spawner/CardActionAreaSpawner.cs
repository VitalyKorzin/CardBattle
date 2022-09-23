using System;
using UnityEngine;

public class CardActionAreaSpawner : MonoBehaviour
{
    [SerializeField] private CardsDeck _cardsDeck;
    [SerializeField] private CardActionArea _template;

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

        _cardsDeck.CardAdded += OnCardAdded;
    }

    private void OnDisable()
        => _cardsDeck.CardAdded -= OnCardAdded;

    private void OnCardAdded(Card card)
    {
        card.Selected += OnCardSelected;
        card.Destroyed += OnCardDestroyed;
    }

    private void OnCardSelected(Card card) 
        => Instantiate(_template, transform.position, Quaternion.identity).Initialize(card);

    private void OnCardDestroyed(Card card)
    {
        card.Selected -= OnCardSelected;
        card.Destroyed -= OnCardDestroyed;
    }

    private void Validate()
    {
        if (_cardsDeck == null)
            throw new InvalidOperationException();
    }
}