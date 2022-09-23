using UnityEngine;
using System.Collections.Generic;

public interface IPlayerAbility
{
    Sprite Icon { get; }

    void Use(IReadOnlyList<Stickman> stickmen, Vector3 actionPosition);
}