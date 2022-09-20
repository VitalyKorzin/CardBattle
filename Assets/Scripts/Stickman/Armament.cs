using System;
using UnityEngine;
using UnityEngine.Events;

public class Armament : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Transform _swordPosition;
    [SerializeField] private Transform _bowPosition;
    [SerializeField] private Transform _quiverPosition;
    [SerializeField] private ParticleSystem _buff;

    public event UnityAction<Weapon> WeaponGived;

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

    private void Start() => Give(_weapon, false);

    public void Give(Weapon weapon, bool buffPlays = true)
    {
        if (weapon is Bow bow)
            GiveBow(bow, buffPlays);
        else if (weapon is Fists fists)
            Give(fists, buffPlays);
        else if (weapon is Sword sword)
            Give(sword, buffPlays);
        else if (weapon is Wand wand)
            Give(wand, buffPlays);
    }

    public void Give(Fists fists, bool buffPlays = true)
    {
        Give(fists, _swordPosition, buffPlays);
        TryDestroyCurrentQuiver();
    }

    private void Give(Sword sword, bool buffPlays = true)
    {
        Give(sword, _swordPosition, buffPlays);
        TryDestroyCurrentQuiver();
    }

    private void Give(Wand wand, bool buffPlays = true)
    {
        Give(wand, _swordPosition, buffPlays);
        TryDestroyCurrentQuiver();
    }

    private void Give(Bow bow, bool buffPlays = true)
    {
        Give(bow, _bowPosition, buffPlays);
        Dress(bow.Quiver);
    }

    private void Give<T>(T newWeapon, Transform position, bool buffPlays = true) where T: Weapon
    {
        if (newWeapon == null)
            throw new ArgumentNullException(nameof(newWeapon));

        Dress(newWeapon, position);

        if (buffPlays)
            _buff.Play();
    }

    private void Dress<T>(T newWeapon, Transform position) where T: Weapon
    {
        if (_weapon != null && _weapon != newWeapon)
            Destroy(_weapon.gameObject);

        _weapon = Instantiate(newWeapon, position);
        WeaponGived?.Invoke(_weapon);
    }

    private void Dress(Quiver quiver)
    {
        TryDestroyCurrentQuiver();
        _quiver = Instantiate(quiver, _quiverPosition);
    }

    private void GiveBow(Bow bow, bool buffPlays = true)
    {
        Give(bow, buffPlays);
        Dress(bow.Quiver);
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