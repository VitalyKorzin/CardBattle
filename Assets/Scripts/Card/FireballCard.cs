using UnityEngine;

public class FireballCard : Card
{
    [Min(0)]
    [SerializeField] private int _damage;

    protected override void Action(Stickman stickman)
        => stickman.Apply(_damage);
}