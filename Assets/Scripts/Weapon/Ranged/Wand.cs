using System;
using UnityEngine;

public class Wand : Ranged<MagicBall>
{
    protected override void ApplyDamage(Stickman target, Transform shotPoint)
    {
        if (target == null)
            return;

        Projectile projectile = Instantiate(Projectile, shotPoint.position, Quaternion.identity);
        projectile.StartMove(target);
    }

    protected override void PlayAnimation(Animator animator)
    {
        if (animator == null)
            throw new ArgumentNullException(nameof(animator));

        animator.SetTrigger(StickmanAnimator.Params.IsCastsSpell);
    }
}