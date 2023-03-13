using System.Collections.Generic;
using UnityEngine;

public class HeroesSquad : StickmenSquad
{
    [SerializeField] private CardsHand _cardsHand;

    private readonly List<Card> _cards = new List<Card>();

    protected override void OnEnable()
    {
        base.OnEnable();
        _cardsHand.CardAdded += OnCardAdded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _cardsHand.CardAdded -= OnCardAdded;

        foreach (Card card in _cards)
        {
            card.Selected -= OnCardSelected;
            card.Deselected -= OnCardDeselected;
            card.Destroyed -= OnCardDestroyed;
        }
    }

    private void OnCardAdded(Card card)
    {
        if (card is MultiplierCard)
        {
            _cards.Add(card);
            card.Selected += OnCardSelected;
            card.Deselected += OnCardDeselected;
            card.Destroyed += OnCardDestroyed;
        }
    }

    private void OnCardDestroyed(Card card)
    {
        card.Destroyed -= OnCardDestroyed;
        DisablePlaceDisplay();
    }

    private void OnCardDeselected(Card card)
    {
        card.Deselected -= OnCardDeselected;
        DisablePlaceDisplay();
    }

    private void OnCardSelected(Card card)
    {
        card.Selected -= OnCardSelected;
        EnablePlaceDisplay();
    }
}