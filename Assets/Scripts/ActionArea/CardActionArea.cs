using System;
using UnityEngine;

public class CardActionArea : ActionArea
{
    public void Initialize(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        card.Destroyed += OnCardDestroyed;
        card.Deselected += OnCardDeselected;
    }

    private void OnCardDestroyed(Card card)
    {
        DeselectStickmen();
        card.Use(Stickmen, transform.position);
        Destroy(card);
    }

    private void OnCardDeselected(Card card)
    {
        DeselectStickmen();
        Destroy(card);
    }

    private void Destroy(Card card)
    {
        card.Destroyed -= OnCardDestroyed;
        card.Deselected -= OnCardDeselected;
        Destroy(gameObject);
    }

    private void DeselectStickmen()
    {
        foreach (var stickman in Stickmen)
            stickman.Deselect();
    }
}