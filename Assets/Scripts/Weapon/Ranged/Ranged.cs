using System;
using UnityEngine;

public abstract class Ranged<T> : Weapon
    where T: Projectile
{
    [SerializeField] private T _projectile;

    protected T Projectile => _projectile;
    protected Transform ShotPoint { get; private set; }
}