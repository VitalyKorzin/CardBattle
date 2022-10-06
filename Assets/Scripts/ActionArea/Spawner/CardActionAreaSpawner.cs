using UnityEngine;

public class CardActionAreaSpawner : MonoBehaviour
{
    [SerializeField] private CardsHand _cardsHand;
    [SerializeField] private CardActionArea _template;

    private void OnEnable()
        => _cardsHand.CardAdded += OnCardAdded;

    private void OnDisable()
        => _cardsHand.CardAdded -= OnCardAdded;

    private void OnCardAdded(Card card)
    {
        card.Selected += OnCardSelected;
        card.Destroyed += OnCardDestroyed;
    }

    private void OnCardSelected(Card card) 
        => Instantiate(_template, transform.position, Quaternion.identity).Initialize(card);

    private void OnCardDestroyed(Card card)
    {
        card.Selected -= OnCardSelected;
        card.Destroyed -= OnCardDestroyed;
    }
}