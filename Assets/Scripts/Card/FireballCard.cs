using System;
using UnityEngine;
using UnityEngine.Events;

public class FireballCard : Card
{
    public event UnityAction<FireballCard, CardActionArea> Used;

    public override void Use(CardActionArea actionArea)
    {
        if (actionArea == null)
            throw new ArgumentNullException(nameof(actionArea));

        Used?.Invoke(this, actionArea);
    }
}