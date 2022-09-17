using System;
using UnityEngine;

public class Bow : Ranged<Arrow>
{
    [SerializeField] private Quiver _quiver;

    public Quiver Quiver => _quiver;

    protected override void ApplyDamage(Stickman target)
    {
        if (target == null)
            return;

        Projectile projectile = Instantiate(Projectile, ShotPoint.position, Quaternion.identity);
        projectile.StartMove(target);
    }

    protected override void PlayAnimation(Animator animator)
    {
        if (animator == null)
            throw new ArgumentNullException(nameof(animator));

        animator.SetTrigger(StickmanAnimator.Params.IsArchery);
    }

    protected override void Validate()
    {
        base.Validate();

        if (_quiver == null)
            throw new InvalidOperationException();
    }
}