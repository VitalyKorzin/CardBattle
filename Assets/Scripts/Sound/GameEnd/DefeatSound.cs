using UnityEngine;

[RequireComponent(typeof(GameOverScreen))]
public class DefeatSound : Sound
{
    private GameOverScreen _screen;

    private void OnEnable()
        => _screen.Showed += OnScreenShowed;

    private void OnDisable()
        => _screen.Showed -= OnScreenShowed;

    protected override void Awake()
    {
        base.Awake();
        _screen = GetComponent<GameOverScreen>();
    }

    private void OnScreenShowed() => Play();
}