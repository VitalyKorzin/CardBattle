using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireballCard : Card
{
    public event UnityAction<FireballCard, Vector3> Used;

    public override void Use(IReadOnlyList<Stickman> stickmen, Vector3 actionPosition) 
        => Used?.Invoke(this, actionPosition);
}