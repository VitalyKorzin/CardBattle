using System;
using UnityEngine;

public class CardActionAreaSpawner : MonoBehaviour
{
    [SerializeField] private CardsDeck _cardsDeck;
    [SerializeField] private FireballCardActionArea _fireballAreaTemplate;
    [SerializeField] private PlainCardActionArea _plainAreaTemplate;

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
    {
        if (card is FireballCard)
            InstantiateArea(_fireballAreaTemplate, card);
        else
            InstantiateArea(_plainAreaTemplate, card);
    }

    private void InstantiateArea<T>(CardActionArea<T> template, Card card) where T: Stickman
        => Instantiate(template, transform.position, Quaternion.identity).Initialize(card);

    private void OnCardDestroyed(Card card)
    {
        card.Selected -= OnCardSelected;
        card.Destroyed -= OnCardDestroyed;
    }

    private void Validate()
    {
        if (_cardsDeck == null)
            throw new InvalidOperationException();

        if (_fireballAreaTemplate == null)
            throw new InvalidOperationException();

        if (_plainAreaTemplate == null)
            throw new InvalidOperationException();
    }
}