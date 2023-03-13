using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardsSet : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private CardViewInDeck _cardViewTemplate;

    private readonly string _multiplierSymbol = "x";

    private Canvas _canvas;
    private CardsDeck _deck;
    private CardViewInDeck _cardViewInDeck;
    private Card _card;

    public string CardsName => _card.Name;
    public int Count { get; private set; }

    public event UnityAction CardSelected;

    public void Initialize(Canvas canvas, CardsDeck deck, Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        if (canvas == null)
            throw new ArgumentNullException(nameof(canvas));

        if (deck == null)
            throw new ArgumentNullException(nameof(deck));

        if (Count != 0)
            throw new InvalidOperationException();

        _card = card;
        _canvas = canvas;
        _deck = deck;
        Draw();
        AddCount();
    }

    public void Add(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        if (CanAdd(card.Name) == false)
            throw new InvalidOperationException();

        AddCount();
    }

    public bool CanAdd(string cardName)
    {
        if (Count == 0)
            throw new InvalidOperationException();

        return CardsName == cardName;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _cardViewInDeck = Instantiate(_cardViewTemplate, transform.position, transform.rotation, _canvas.transform);
        _cardViewInDeck.Initialize(_canvas, _card);
        CardSelected?.Invoke();
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
        => _cardViewInDeck.Move(eventData.delta);

    public void OnEndDrag(PointerEventData eventData)
    {
        var result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);

        foreach (var item in result)
        {
            if (item.gameObject.TryGetComponent(out SelectedCardSlot slot))
            {
                slot.Set(_cardViewInDeck);
                RemoveCount();
                _deck.Remove(_card);
                return;
            }
        }

        Destroy(_cardViewInDeck.gameObject);
    }

    private void Draw()
    {
        if (_card.TryGetComponent(out CardView view))
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

    private void AddCount()
    {
        Count++;
        DrawCount();
    }

    private void RemoveCount()
    {
        Count--;
        DrawCount();
    }

    private void DrawCount() => _countText.text = _multiplierSymbol + Count;
}