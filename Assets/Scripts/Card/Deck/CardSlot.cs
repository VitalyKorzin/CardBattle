using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CardSlot : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _cardPositionY;
    [Min(0)]
    [SerializeField] private float _rotationDuration;

    private Vector3 _cardPosition;

    public event UnityAction<CardSlot> Destroyed;

    public void Initialize(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        card.Destroyed += OnCardDestroyed;
        card.Deselected += OnCardDeselected;
        card.transform.parent = transform;
        _cardPosition = new Vector3(0f, _cardPositionY, 0f);
        card.MoveWithOffset(_cardPosition);
    }

    public void Rotate(float targetAngle)
    {
        var targetRotation = new Vector3(0f, 0f, targetAngle);
        transform.DOLocalRotate(targetRotation, _rotationDuration);
    }

    private void OnCardDeselected(Card card)
    {
        card.transform.parent = transform;
        card.MoveBack(_cardPosition);
    }

    private void OnCardDestroyed(Card card)
    {
        card.Destroyed -= OnCardDestroyed;
        card.Deselected -= OnCardDeselected;
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}