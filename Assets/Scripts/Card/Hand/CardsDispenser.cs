using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDispenser : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _secondsBetweenDispensing;
    [SerializeField] private CardsHand _cardsHand;
    [SerializeField] private Menu _menu;
    [SerializeField] private SelectedCardSlot[] _slots;

    private readonly List<Card> _cards = new List<Card>();

    public int CardsCount => _cards.Count;

    private void OnEnable()
    {
        _menu.GameBegun += OnGameBegun;

        foreach (SelectedCardSlot slot in _slots)
        {
            slot.Added += OnCardAdded;
            slot.Removed += OnCardRemoved;
        }
    }

    private void OnDisable()
    {
        _menu.GameBegun -= OnGameBegun;

        foreach (SelectedCardSlot slot in _slots)
        {
            slot.Added -= OnCardAdded;
            slot.Removed -= OnCardRemoved;
        }
    }

    public void OnGameBegun() => StartCoroutine(Dispense());

    private IEnumerator Dispense()
    {
        var _delay = new WaitForSeconds(_secondsBetweenDispensing);

        for (var i = 0; i < _cards.Count; i++)
        {
            var card = Instantiate(_cards[i], transform.position, Quaternion.identity, _cardsHand.transform);
            _cardsHand.Add(card);
            yield return _delay;
        }
    }

    private void OnCardAdded(Card card) => _cards.Add(card);

    private void OnCardRemoved(Card card) => _cards.Remove(card);
}