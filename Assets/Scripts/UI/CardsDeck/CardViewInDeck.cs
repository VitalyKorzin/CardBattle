using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CardViewInDeck : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;
    [SerializeField] private Image _frame;

    private Canvas _canvas;
    private RectTransform _rectTransform;

    public Card Card { get; private set; }

    private void Awake() 
        => _rectTransform = GetComponent<RectTransform>();

    public void Initialize(Canvas canvas, Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        if (canvas == null)
            throw new ArgumentNullException(nameof(canvas));

        if (Card != null)
            throw new InvalidOperationException();

        _canvas = canvas;
        Card = card;
        Draw();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);

        foreach (var item in result)
        {
            if (item.gameObject.TryGetComponent(out SelectedCardSlot slot))
            {
                transform.parent = _canvas.transform;
                slot.Remove();
            }
        }

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData) => Move(eventData.delta);

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent != _canvas.transform)
            return;

        var result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);

        foreach (var item in result)
        {
            if (item.gameObject.TryGetComponent(out CardsDeck cardsDeck))
            {
                cardsDeck.Add(Card);
                Destroy(gameObject);
                return;
            }

            if (item.gameObject.TryGetComponent(out SelectedCardSlot slot))
            {
                slot.Set(this);
                return;
            }
        }
    }

    public void Move(Vector2 delta)
    {
        if (transform.parent == _canvas.transform)
            _rectTransform.anchoredPosition += delta / _canvas.scaleFactor;
    }

    private void Draw()
    {
        if (Card.TryGetComponent(out CardView view))
        {
            _icon.sprite = view.Icon;
            _background.sprite = view.Background;
            _background.color = view.BackgroundColor;
            _frame.sprite = view.Frame;
            _frame.color = view.FrameColor;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }
}