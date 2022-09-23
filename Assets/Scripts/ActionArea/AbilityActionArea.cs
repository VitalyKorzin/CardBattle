using System;
using UnityEngine;

public class AbilityActionArea : ActionArea
{
    public void Use(IPlayerAbility playerAbility)
    {
        if (playerAbility == null)
            throw new ArgumentNullException(nameof(playerAbility));

        playerAbility.Use(Stickmen, transform.position);
    }
}