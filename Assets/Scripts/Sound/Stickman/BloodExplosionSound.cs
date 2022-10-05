using UnityEngine;

[RequireComponent(typeof(Blood))]
public class BloodExplosionSound : Sound
{
    private Blood _blood;

    private void OnEnable() 
        => _blood.Released += OnBloodReleased;

    private void OnDisable()
        => _blood.Released -= OnBloodReleased;

    protected override void Awake()
    {
        base.Awake();
        _blood = GetComponent<Blood>();
    }

    private void OnBloodReleased() => Play();
}