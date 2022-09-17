using System;
using UnityEngine;

public class Fists : Melee
{
    protected override void ApplyDamage(Stickman target)
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