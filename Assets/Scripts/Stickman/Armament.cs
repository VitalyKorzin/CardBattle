using System;
using UnityEngine;

[RequireComponent(typeof(AttackState))]
public class Armament : MonoBehaviour
{
    [SerializeField] private Fists _fists;
    [SerializeField] private Sword _sword;
    [SerializeField] private Wand _wand;
    [SerializeField] private Bow _bow;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Transform _swordPosition;
    [SerializeField] private Transform _bowPosition;
    [SerializeField] private Transform _quiverPosition;
    [SerializeField] private ParticleSystem _buff;

    private AttackState _attackState;
    private Weapon _currentWeapon;

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

    private void Awake()
    {
        _attackState = GetComponent<AttackState>();
        TryDressWeapon();
    }

    public void Give(Sword sword)
    {
        Give(sword, _swordPosition);
        TryDestroyCurrentQuiver();
    }

    public void Give(Wand wand)
    {
        Give(wand, _swordPosition);
        TryDestroyCurrentQuiver();
    }

    public void Give(Bow bow)
    {
        Give(bow, _bowPosition);
        Dress(bow.Quiver);
    }

    private void Give<T>(T newWeapon, Transform position) where T: Weapon
    {
        if (newWeapon == null)
            throw new ArgumentNullException(nameof(newWeapon));

        Dress(newWeapon, position);
        _buff.Play();
    }

    private void Dress<T>(T newWeapon, Transform position) where T: Weapon
    {
        if (_currentWeapon != null)
            Destroy(_currentWeapon.gameObject);

        _currentWeapon = Instantiate(newWeapon, position);
        _attackState.Initialize(newWeapon);
    }

    private void Dress(Quiver quiver)
    {
        TryDestroyCurrentQuiver();
        _quiver = Instantiate(quiver, _quiverPosition);
    }

    private void TryDressWeapon()
    {
        if (_fists != null)
            Dress(_fists, _swordPosition);

        if (_sword != null)
            Dress(_sword, _swordPosition);

        if (_wand != null)
            Dress(_wand, _swordPosition);

        if (_bow != null)
        {
            Dress(_bow, _bowPosition);
            Dress(_bow.Quiver);
        }
    }

    private void TryDestroyCurrentQuiver()
    {
        if (_quiver != null)
            Destroy(_quiver.gameObject);
    }

    private void Validate()
    {
        if (_swordPosition == null)
            throw new InvalidOperationException();

        if (_bowPosition == null)
            throw new InvalidOperationException();

        if (_quiverPosition == null)
            throw new InvalidOperationException();

        if (_buff == null)
            throw new InvalidOperationException();
    }
}