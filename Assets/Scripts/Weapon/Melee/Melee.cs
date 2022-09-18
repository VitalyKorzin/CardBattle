using UnityEngine;

public abstract class Melee : Weapon
{
    [Min(0)]
    [SerializeField] private int _damage;

    public int Damage => _damage;
}