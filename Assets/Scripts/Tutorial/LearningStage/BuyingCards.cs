using UnityEngine;
using UnityEngine.UI;

public class BuyingCards : LearningStage
{
    [SerializeField] private CardsDeck _cardsDeck;
    [SerializeField] private Button _closeShopButton;

    private readonly int _neededCardsBoughtCount = 3;

    private int _cardsBought;

    private void OnEnable()
    {
        _cardsDeck.Added += OnCardBought;
        _closeShopButton.interactable = false;
    }

    private void OnDisable()
    {
        _cardsDeck.Added -= OnCardBought;
        _closeShopButton.interactable = true;
    }

    private void OnCardBought()
    {
        if (_cardsBought < _neededCardsBoughtCount)
        {
            _cardsBought++;

            if (_cardsBought == _neededCardsBoughtCount)
                Finish();
        }
    }
}