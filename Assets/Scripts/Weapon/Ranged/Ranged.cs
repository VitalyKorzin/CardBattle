using System;
using UnityEngine;

public abstract class Ranged<T> : Weapon
    where T: Projectile
{
    [SerializeField] private T _projectile;
    [SerializeField] private Transform _shotPoint;

    protected T Projectile => _projectile;
    protected Transform ShotPoint => _shotPoint;

    protected override void Validate()
    {
        base.Validate();

        if (_projectile == null)
            throw new InvalidOperationException();

        if (_shotPoint == null)
            throw new InvalidOperationException();
    }
}