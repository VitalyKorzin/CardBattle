using UnityEngine;

[RequireComponent(typeof(Confetti))]
public class ConfettiSound : Sound
{
    private Confetti _confetti;

    private void OnEnable() 
        => _confetti.Blasted += OnConfettiBlasted;

    private void OnDisable() 
        => _confetti.Blasted -= OnConfettiBlasted;

    protected override void Awake()
    {
        base.Awake();
        _confetti = GetComponent<Confetti>();
    }

    private void OnConfettiBlasted() => Play();
}