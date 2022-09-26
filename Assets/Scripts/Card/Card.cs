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
    [Min(0)]
    [SerializeField] private int _price;
    [SerializeField] private string _name;

    private CardMover _mover;
    private CardView _view;
    private CardRotator _rotator;
    private CardScaler _scaler;
    private Camera _camera;
    private bool _isSelected;
    private Ray _ray;
    private RaycastHit[] _hits;

    public string Name => _name;
    public int Price => _price;

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

    public abstract void Use(IReadOnlyList<Stickman> stickmen, Vector3 actionPosition);

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
        _rotator.Rotate();
        Selected?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _ray = _camera.ScreenPointToRay(eventData.position);
        _hits = Physics.RaycastAll(_ray);

        foreach (var hit in _hits)
        {
            if (hit.collider.TryGetComponent(out CardsHand _))
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
        _rotator.Rotate();
        _view.ChangeColorOnDeselection();
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delayBeforeDestroy);
        Destroy(gameObject);
    }
}