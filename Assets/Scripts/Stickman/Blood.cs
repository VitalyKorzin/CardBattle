using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Blood : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _bloodParticles;

    private readonly float _delay = 8f;

    public event UnityAction Released;

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

    public void Draw()
    {
        transform.parent = null;

        foreach (ParticleSystem blood in _bloodParticles)
            blood.Play();

        Released?.Invoke();
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }

    private void Validate()
    {
        if (_bloodParticles == null)
            throw new InvalidOperationException();

        if (_bloodParticles.Length == 0)
            throw new InvalidOperationException();

        foreach (ParticleSystem blood in _bloodParticles)
            if (blood == null)
                throw new InvalidOperationException();
    }
}