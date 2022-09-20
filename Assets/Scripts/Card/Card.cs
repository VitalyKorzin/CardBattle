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
    private Camera _camera;
    private bool _isSelected;

    public event UnityAction<Card> Selected;
    public event UnityAction<Card> Deselected;
    public event UnityAction<Card> Destroyed;

    private void Awake()
    {
        _mover = GetComponent<CardMover>();
        _view = GetComponent<CardView>();
        _rotator = GetComponent<CardRotator>();
        _scaler = GetComponent<CardScaler>();
        _camera = Camera.main;
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
        _view.ChangeColorOnSelection();
        _scaler.ChangeSize();
        _mover.MoveOnSelection();
        _rotator.RotateOnSelection();
        Selected?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var _ray = _camera.ScreenPointToRay(eventData.position);
        var _hits = Physics.RaycastAll(_ray);

        foreach (var hit in _hits)
        {
            if (hit.collider.TryGetComponent(out CardsDeck _))
            {
                Deselected?.Invoke(this);
                _isSelected = false;
                return;
            }
        }

        _view.Fade();
        Destroyed?.Invoke(this);
        StartCoroutine(Destroy());
    }

    public void MoveWithOffset(Vector3 targetPosition)
        => _mover.MoveWithOffset(targetPosition);

    public void MoveBack(Vector3 targetPosition)
    {
        _mover.MoveOnDeselection(targetPosition);
        _rotator.RotateOnDeselection();
        _view.ChangeColorOnDeselection();
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delayBeforeDestroy);
        Destroy(gameObject);
    }
}