using UnityEngine;
using System.Collections.Generic;

public interface IPlayerAbility
{
    void Use(IReadOnlyList<Stickman> stickmen, Vector3 actionPosition);
}