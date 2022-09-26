using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CardsDeck : Window
{
    [SerializeField] private List<Card> _cards;
    [SerializeField] private CardsSet _cardsSetTemplate;
    [SerializeField] private Transform _content;
    [SerializeField] private Canvas _canvas;

    private readonly List<CardsSet> _sets = new List<CardsSet>();

    protected override void Awake()
    {
        base.Awake();

        foreach (Card card in _cards)
            Add(card);
    }

    public void Add(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        if (ContainsCardsSet(card.Name, out CardsSet cardsSet))
        {
            if (cardsSet.CanAdd(card.Name))
                cardsSet.Add(card);
        }
        else
        {
            CardsSet set = Instantiate(_cardsSetTemplate, _content);
            set.Initialize(_canvas, this, card);
            _sets.Add(set);
        }
    }

    public void Remove(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        if (ContainsCardsSet(card.Name, out CardsSet cardsSet))
        {
            if (cardsSet.Count == 0)
            {
                _sets.Remove(cardsSet);
                Destroy(cardsSet.gameObject);
            }
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public bool ContainsCardsSet(string cardName) 
        => ContainsCardsSet(cardName, out CardsSet _);

    private bool ContainsCardsSet(string cardName, out CardsSet cardsSet)
    {
        cardsSet = _sets.FirstOrDefault(cardsSet => cardsSet.CardsName == cardName);
        return cardsSet != null;
    }
} 