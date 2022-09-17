using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _secondsBetweenAttack;
    [Min(0)]
    [SerializeField] private float _transitionRange;

    public float TransitionRange => _transitionRange;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch (Exception exception)
        {
            enabled = false;
            throw exception;
        }
    }

    public IEnumerator Attack(Stickman target, Animator animator)
    {
        PlayAnimation(animator);
        yield return new WaitForSeconds(_secondsBetweenAttack); ;
        ApplyDamage(target);
    }

    protected abstract void PlayAnimation(Animator animator);

    protected abstract void ApplyDamage(Stickman target);

    protected virtual void Validate() { }
}