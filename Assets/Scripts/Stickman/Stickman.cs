using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Stickman : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _health;
    [Min(1)]
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem[] _bloodParticles;
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private ParticleSystem _buff;
    [SerializeField] private Transform _swordPosition;
    [SerializeField] private Transform _shieldPosition;
    [SerializeField] private Transform _helmetPosition;
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Material _highlightMaterial;

    private Material _defaultMaterial;

    public int Health => _health;
    public int Damage => _damage;

    public event UnityAction<int> HealthChanged;
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
        => _defaultMaterial = _renderer.material;

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

    public void GiveArmor(Shield shield, Helmet helmet, int additionalHealth, int additionalDamage)
    {
        if (shield == null)
            throw new ArgumentNullException(nameof(shield));

        if (helmet == null)
            throw new ArgumentNullException(nameof(helmet));

        Instantiate(shield, _shieldPosition);
        Instantiate(helmet, _helmetPosition);
        Strengthen(additionalHealth, additionalDamage);
    }

    public void GiveWeapon(Sword sword, int additionalHealth, int additionalDamage)
    {
        if (sword == null)
            throw new ArgumentNullException(nameof(sword));

        Instantiate(sword, _swordPosition);
        Strengthen(additionalHealth, additionalDamage);
    }

    public void Apply(int damage)
    {
        if (_health <= 0)
            return;

        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _hit.Play();
        _health -= damage;
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Die();
    }

    public void Heal(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _health += value;
        HealthChanged?.Invoke(_health);
    }

    private void Strengthen(int additionalHealth, int additionalDamage)
    {
        if (additionalHealth < 0)
            throw new ArgumentOutOfRangeException(nameof(additionalHealth));

        if (additionalDamage < 0)
            throw new ArgumentOutOfRangeException(nameof(additionalDamage));

        _buff.Play();
        Heal(additionalHealth);
        _damage += additionalDamage;
    }

    private void Die()
    {
        foreach (var blood in _bloodParticles)
        {
            blood.transform.parent = null;
            blood.Play();
        }

        Died?.Invoke(this);
    }

    private void Validate()
    {
        if (_bloodParticles == null)
            throw new InvalidOperationException();

        if (_bloodParticles.Length == 0)
            throw new InvalidOperationException();

        if (_hit == null)
            throw new InvalidOperationException();

        if (_buff == null)
            throw new InvalidOperationException();

        if (_swordPosition == null)
            throw new InvalidOperationException();

        if (_shieldPosition == null)
            throw new InvalidOperationException();

        if (_helmetPosition == null)
            throw new InvalidOperationException();

        if (_renderer == null)
            throw new InvalidOperationException();
    }
}