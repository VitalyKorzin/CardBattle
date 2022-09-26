using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : Window
{
    [SerializeField] private List<Card> _cards;
    [SerializeField] private ShopItemCardView _itemView;
    [SerializeField] private Transform _content;
    [SerializeField] private Wallet _wallet;

    private readonly List<ShopItemCardView> _items = new List<ShopItemCardView>();

    public event UnityAction<Card> CardBought;

    private void OnDisable()
    {
        foreach (ShopItemCardView item in _items)
            item.SellButtonClick -= OnSellButtonClick;
    }

    protected override void Awake()
    {
        base.Awake();

        foreach (Card card in _cards)
        {
            ShopItemCardView item = Instantiate(_itemView, _content);
            item.Initialize(card);
            _items.Add(item);
            item.SellButtonClick += OnSellButtonClick;
        }
    }

    private void OnSellButtonClick(Card card)
    {
        if (_wallet.CheckSolvency(card.Price))
        {
            _wallet.Withdraw(card.Price);
            CardBought?.Invoke(card);
        }
    }
}