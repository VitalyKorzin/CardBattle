using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectedCardSlot : MonoBehaviour
{
    [SerializeField] private CardsDeck _cardsDeck;
    [SerializeField] private CardViewInDeck _cardViewTemplate;
    [SerializeField] private Card _startCard;
    [SerializeField] private Canvas _canvas;

    private CardViewInDeck _cardView;

    public event UnityAction<Card> Added;
    public event UnityAction Selected;
    public event UnityAction<Card> Removed;

    private void Start()
    {
        if (_startCard == null)
            return;

        Added?.Invoke(_startCard);
        _cardView = Instantiate(_cardViewTemplate, transform);
        _cardView.Initialize(_canvas, _startCard);
    }

    public void Set(CardViewInDeck cardView)
    {
        if (cardView == null)
            throw new ArgumentNullException(nameof(cardView));

        if (_cardView != null)
        {
            _cardsDeck.Add(_cardView.Card);
            Removed?.Invoke(_cardView.Card);
            Destroy(_cardView.gameObject);
        }

        cardView.transform.parent = transform;
        cardView.transform.localPosition = Vector3.zero;
        _cardView = cardView;
        Added?.Invoke(_cardView.Card);
        Selected?.Invoke();
    }

    public void Remove()
    {
        if (_cardView == null)
            throw new InvalidOperationException();

        Removed?.Invoke(_cardView.Card);
        _cardView = null;
    }
}