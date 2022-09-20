using UnityEngine;

public class MagicBall : Projectile
{
    [SerializeField] private ParticleSystem _hit;

    protected override void DisplayHit() 
        => Instantiate(_hit, transform.position, Quaternion.identity);
}