using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardsSelectionSlots : MonoBehaviour
{
    [SerializeField] private List<SelectedCardSlot> _slots;

    private readonly int _needSelectedCardsCount = 3;

    private int _selectedCards;

    public event UnityAction CardsSelected;

    private void OnEnable()
    {
        foreach (SelectedCardSlot slot in _slots)
        {
            slot.Added += OnCardAdded;
            slot.Removed += OnCardRemoved;
        }
    }

    private void OnDisable()
    {
        foreach (SelectedCardSlot slot in _slots)
        {
            slot.Added -= OnCardAdded;
            slot.Removed -= OnCardRemoved;
        }
    }

    private void OnCardAdded(Card card)
    {
        _selectedCards++;

        if (_selectedCards == _needSelectedCardsCount)
            CardsSelected?.Invoke();
    }

    private void OnCardRemoved(Card card) => _selectedCards--;
}