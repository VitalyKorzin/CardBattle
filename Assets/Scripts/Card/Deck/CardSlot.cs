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

    public event UnityAction<CardSlot> Destroyed;

    public void Initialize(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        card.Destroyed += OnCardDestroyed;
        card.transform.parent = transform;
        var cardPosition = new Vector3(0f, _cardPositionY, 0f);
        card.MoveWithOffset(cardPosition);
    }

    public void Rotate(float targetAngle)
    {
        var targetRotation = new Vector3(0f, 0f, targetAngle);
        transform.DOLocalRotate(targetRotation, _rotationDuration);
    }

    private void OnCardDestroyed(Card card)
    {
        card.Destroyed -= OnCardDestroyed;
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}