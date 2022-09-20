using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(CardMover), typeof(CardView), typeof(CardRotator))]
[RequireComponent(typeof(CardScaler))]
public abstract class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Min(0)]
    [SerializeField] private float _delayBeforeDestroy;

    private CardMover _mover;
    private CardView _view;
    private CardRotator _rotator;
    private CardScaler _scaler;
    private bool _isSelected;

    public event UnityAction<Card> Selected;
    public event UnityAction<Card> Destroyed;

    private void Awake()
    {
        _mover = GetComponent<CardMover>();
        _view = GetComponent<CardView>();
        _rotator = GetComponent<CardRotator>();
        _scaler = GetComponent<CardScaler>();
    }

    public abstract void Use<T>(List<T> stickmen, Vector3 actionPosition)
        where T : Stickman;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isSelected)
            return;

        if (transform.parent.parent != null)
            transform.parent = transform.parent.parent;

        _isSelected = true;
        _view.ChangeColor();
        _scaler.ChangeSize();
        _mover.MoveOnSelection();
        _rotator.Rotate();
        Selected?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _view.Fade();
        Destroyed?.Invoke(this);
        StartCoroutine(Destroy());
    }

    public void MoveWithOffset(Vector3 targetPosition)
        => _mover.MoveWithOffset(targetPosition);

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delayBeforeDestroy);
        Destroy(gameObject);
    }
}