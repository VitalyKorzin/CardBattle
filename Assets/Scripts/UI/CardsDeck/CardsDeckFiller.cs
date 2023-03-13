using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CardsDeckFiller : MonoBehaviour
{
    [SerializeField] private CardsDeckSaver _saver;
    [SerializeField] private List<Card> _cards;

    public Card GetCard(string slotName)
    {
        string cardName = _saver.LoadSelectedCard(slotName);

        if (TryGetCard(cardName, out Card card))
            return card;
        else
            return null;
    }

    public List<Card> GetCards()
    {
        var cards = new List<Card>();

        foreach (Card card in _cards)
            for (int i = 0; i < _saver.LoadCardsSet(card.Name); i++)
                cards.Add(card);

        return cards;
    }

    private bool TryGetCard(string cardName, out Card card)
    {
        card = _cards.FirstOrDefault(result => result.Name == cardName);
        return card != null;
    }
}