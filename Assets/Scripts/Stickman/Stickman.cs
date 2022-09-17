using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health), typeof(Protection))]
public abstract class Stickman : MonoBehaviour
{
    [SerializeField] private Blood _blood;
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Material _highlightMaterial;

    private Material _defaultMaterial;
    private Health _health;
    private Protection _armor;

    public event UnityAction<Stickman> Died;
    public event UnityAction<StickmenSquad> FightStarted;
    public event UnityAction<PlaceInSquad> AddedToSquad;

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

    private void Start()
    {
        _health = GetComponent<Health>();
        _armor = GetComponent<Protection>();
        _defaultMaterial = _renderer.material;
    }

    public void Select()
        => _renderer.material = _highlightMaterial;

    public void Deselect()
        => _renderer.material = _defaultMaterial;

    public void AddToSquad(PlaceInSquad place)
    {
        if (place == null)
            throw new ArgumentNullException(nameof(place));

        AddedToSquad?.Invoke(place);
    }

    public void StartFight(StickmenSquad enemies)
    {
        if (enemies == null)
            throw new ArgumentNullException(nameof(enemies));

        FightStarted?.Invoke(enemies);
    }

    public void Apply(int damage)
    {
        if (_health.Value <= 0)
            return;

        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _hit.Play();
        Take(damage);

        if (_health.Value <= 0)
            Die();
    }

    private void Take(int damage)
    {
        if (_armor.Value <= 0)
        {
            _health.Apply(damage);
        }
        else if (_armor.Value >= damage)
        {
            _armor.Apply(damage);
        }
        else
        {
            _health.Apply(damage - _armor.Value);
            _armor.Apply(_armor.Value);
        }
    }

    private void Die()
    {
        _blood.Draw();
        Died?.Invoke(this);
    }

    private void Validate()
    {
        if (_blood == null)
            throw new InvalidOperationException();

        if (_hit == null)
            throw new InvalidOperationException();

        if (_renderer == null)
            throw new InvalidOperationException();

        if (_highlightMaterial == null)
            throw new InvalidOperationException();
    }
}