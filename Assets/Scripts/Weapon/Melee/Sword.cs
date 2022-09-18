using System;
using UnityEngine;

public class Sword : Melee
{
    protected override void ApplyDamage(Stickman target, Transform shotPoint)
    {
        if (target == null)
            return;

        target.Apply(Damage);
    }

    protected override void PlayAnimation(Animator animator)
    {
        if (animator == null)
            throw new ArgumentNullException(nameof(animator));

        animator.SetTrigger(StickmanAnimator.Params.IsAttacking);
    }
}