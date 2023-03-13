using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardsHand : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _maximumCount;
    [Min(0)]
    [SerializeField] private float _rangeAngle;
    [SerializeField] private CardSlot _cardSlot;
    [SerializeField] private CardsDispenser _dispenser;

    private readonly List<CardSlot> _slots = new List<CardSlot>();

    private float _angleBetweenCards;
    private float _pointerAngle;
    private float _angleStep;

    public int MaximumCount => _maximumCount;

    public event UnityAction<Card> CardAdded;
    public event UnityAction Ended;

    private void Awake()
    {
        _angleBetweenCards = _rangeAngle / (_maximumCount - 1);
        _angleStep = _angleBetweenCards / 2f;
        _pointerAngle = _angleBetweenCards;
    }

    public void Add(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        if (_slots.Count == _maximumCount)
            throw new InvalidOperationException();

        CreateSlot(card);
        RotateSlots(_pointerAngle);
        ChangePointerAngle(_angleStep);
        CardAdded?.Invoke(card);
    }

    private void CreateSlot(Card card)
    {
        var slot = Instantiate(_cardSlot, transform);
        slot.Initialize(card);
        _slots.Add(slot);
        slot.Destroyed += OnSlotDestroyed;
    }

    private void OnSlotDestroyed(CardSlot slot)
    {
        slot.Destroyed -= OnSlotDestroyed;
        _slots.Remove(slot);
        RotateSlots(_pointerAngle);
        ChangePointerAngle(-_angleStep);

        if (_slots.Count == 0)
            Ended?.Invoke();
    }

    private void RotateSlots(float pointerAngle)
    {
        foreach (var _slot in _slots)
        {
            pointerAngle -= _angleBetweenCards;
            _slot.Rotate(pointerAngle);
        }
    }

    private void ChangePointerAngle(float angleStep)
    {
        _pointerAngle += angleStep;

        if (_slots.Count == _dispenser.CardsCount)
            _pointerAngle -= _angleBetweenCards;
    }
}