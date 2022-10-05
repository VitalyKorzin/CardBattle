using UnityEngine;

public class BonusSound : Sound
{
    [SerializeField] private CardsHand _cardsHand;

    private void OnEnable() 
        => _cardsHand.CardAdded += OnCardAdded;

    private void OnDisable() 
        => _cardsHand.CardAdded -= OnCardAdded;

    private void OnCardAdded(Card card)
    {
        if (card is WeaponCard || card is ArmorCard)
            card.Destroyed += OnCardDestroyed;
    }

    private void OnCardDestroyed(Card card)
    {
        card.Destroyed -= OnCardDestroyed;
        Play();
    }
}