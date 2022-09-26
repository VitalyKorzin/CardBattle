using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemCardView : MonoBehaviour
{
    [SerializeField] private Image _cardViewBackground;
    [SerializeField] private Image _cardViewIcon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private TMP_Text _price;

    private Card _card;

    public event UnityAction<Card> SellButtonClick;

    private void OnEnable() 
        => _sellButton.onClick.AddListener(OnSellButtonClick);

    private void OnDisable() 
        => _sellButton.onClick.RemoveListener(OnSellButtonClick);

    public void Initialize(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        _card = card;
        Draw();
    }

    private void Draw()
    {
        if (_card.TryGetComponent(out CardView cardView))
        {
            _cardViewBackground.color = cardView.BackgroundColor;
            _cardViewIcon.sprite = cardView.Icon;
            _price.text = _card.Price.ToString();
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    private void OnSellButtonClick() => SellButtonClick?.Invoke(_card);
}