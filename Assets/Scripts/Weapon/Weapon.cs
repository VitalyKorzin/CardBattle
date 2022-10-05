using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _secondsBetweenAttack;
    [Min(0)]
    [SerializeField] private float _transitionRange;

    public float TransitionRange => _transitionRange;

    public event UnityAction Used;

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

    public IEnumerator Attack(Stickman target, Animator animator, Transform shotPoint)
    {
        PlayAnimation(animator);
        yield return new WaitForSeconds(_secondsBetweenAttack); ;
        ApplyDamage(target, shotPoint);
        Used?.Invoke();
    }

    protected abstract void PlayAnimation(Animator animator);

    protected abstract void ApplyDamage(Stickman target, Transform shotPoint);

    protected virtual void Validate() { }
}