using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardsDeck : Window
{
    [SerializeField] private CardsSet _cardsSetTemplate;
    [SerializeField] private Transform _content;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Shop _shop;
    [SerializeField] private CardsDeckSaver _saver;
    [SerializeField] private CardsDeckFiller _filler;

    private readonly List<CardsSet> _sets = new List<CardsSet>();

    public event UnityAction Added;

    private void OnEnable() => _shop.CardBought += OnCardBought;

    private void OnDisable() => _shop.CardBought -= OnCardBought;

    protected override void Awake()
    {
        base.Awake();

        foreach (Card card in _filler.GetCards())
            Add(card, false, false);
    }

    public void Add(Card card, bool notifyWhenAdded = true, bool saveCard = true)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        int currentCount;

        if (ContainsCardsSet(card.Name, out CardsSet cardsSet))
        {
            if (cardsSet.CanAdd(card.Name))
                cardsSet.Add(card);

            currentCount = cardsSet.Count;
        }
        else
        {
            CardsSet set = Instantiate(_cardsSetTemplate, _content);
            set.Initialize(_canvas, this, card);
            _sets.Add(set);
            currentCount = set.Count;
        }

        if (notifyWhenAdded)
            Added?.Invoke();

        if (saveCard)
            _saver.SaveCardsSet(card.Name, currentCount);
    }

    public void Remove(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        if (ContainsCardsSet(card.Name, out CardsSet cardsSet))
        {
            _saver.SaveCardsSet(card.Name, cardsSet.Count);

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

    private void OnCardBought(Card card) => Add(card);
} 